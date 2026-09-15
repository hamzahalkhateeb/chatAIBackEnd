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
    [Route("chat")]
    public class ChatController : ControllerBase
    {
         private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        //dependancy inject services from other files
        //normal classes have to be injected here while static classes can be used directly 
        public ChatController(AuthService authService, UserService userService, IConfiguration config, AppDbContext db)
        {
            _authService = authService;
            _userService = userService;
            _db = db;
            _config = config;
        }


        [HttpPost("CreateChat")]
        public async Task<IActionResult> CreateChat(JsonContent request)
        {
            return Ok();
        }
    }

}