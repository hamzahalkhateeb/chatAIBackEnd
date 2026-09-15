/*
1- get current loggin user profile
//get request comes in
//check the token and verify it
//get the user id from the token
//retrieve the details and send it back

2- get another users public profile
//get request comes in, includes the requested user id
// check the access token if its valid
// retrieve user data and send it back

3- search the users table to start a new chat
// get request comes it includes username and token
//verify token
//use fuzzy search to look for users with similar user names
//send top 3 results

4- update current logged in users profile
//patch request comes in, includes fields which needs changing
//verify token and ensure the to be edited user id matches the id in the request
//execute the changes and send back success

5- deactivate or delete own profile
//delete request comes in with a user id
//verify token and make sure its the same id as in the request
//set the disabled at date and time

6- pin user
//post request comes in with user id, and chat id
//verify token, ensure the id in the token is also in the chat members table
//set the pinned user fiel of the chat to that user id



*/

//pronanly will be swaped for signal, more planning needed

/*
1- create a new chat 1 on 1 or gc

2- retreive all chats for logged in user

3- get a particular chat's details

4- update chat name

5- leave chat
*/

using Microsoft.AspNetCore.Mvc;
using backEnd.Modules;
using backEnd.Modules.Authentication;
using backEnd.Modules.Utils;
using backEnd.Modules.User;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Identity.Data;
using System.Text.Json;
using backEnd.Data;
using Microsoft.EntityFrameworkCore;
namespace backEnd.Modules.Chat
{


    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
         private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        //dependancy inject services from other files
        //normal classes have to be injected here while static classes can be used directly 
        public UserController(AuthService authService, UserService userService, IConfiguration config, AppDbContext db)
        {
            _authService = authService;
            _userService = userService;
            _db = db;
            _config = config;
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsersViaUserName([FromQuery] string UserName)
        {
            if (EnvConfig.IsDebuggingLogging)
                Console.WriteLine($"UserController.GetUsersViaUserName Endpoint: endpoint reached, UserName received: {UserName}");

            //check if user is authenticated

            //if not, return Unauthorized()    

            //call find users via a service
            var FoundUsers = await _userService.FindUserViaUserName(UserName);

            if(FoundUsers == null)
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"UserController.GetUsersViaUserName Endpoint: no users found, returning 200");
                
                return Ok(new {message= "no users found"});
            }

            if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"UserController.GetUsersViaUserName Endpoint: users found: {FoundUsers}");
            return Ok(new {message = "Found users", FoundUsers});
        }
    }

}