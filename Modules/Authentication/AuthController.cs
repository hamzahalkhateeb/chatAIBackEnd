/*
1- register user
//post request comes in
//contains password, email, username
//check if email exists, if yes, send error and redirect to log in page
//if no, validate all data is correct, then
//  create a user using email, username, hash password
//issue 2 tokens


2- log in
//post request comes in
//contains email and password
//look for email, if it doesnt exist, send error
//if email exists, hash password and compared hashes strings
//if hashed match, assign 2 tokens and send them to log in
//if no match, send error

3- refresh token
//post comes in, going to have acces token and refresh token, if refresh token is valid, recreate an access token and send it back

4- log out
//post request comes, with email
//check and verify access token, if the email matches the id present in the token, revoke that particular token
//if not, send an error message

*/


using Microsoft.AspNetCore.Mvc;
using backEnd.Modules;
using backEnd.Modules.Authentication;
using backEnd.Modules.Utils;
using backEnd.Modules.User;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Identity.Data;


namespace backEnd.Modules.Authentication
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;

        //dependancy inject services from other files
        //normal classes have to be injected here while static classes can be used directly 
        public AuthController(AuthService authService, UserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        
        

        //log in
        //post request comes in
        //contains email and password
        //look for email, if it doesnt exist, send error
        //if email exists, hash password and compared hashes strings
        //if hashed match, assign 2 tokens and send them to log in
        //if no match, send error
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            Console.WriteLine($"AuthController.loginEndpoint: endpoint reached, attempted log in with email: {request.Email}, password: {request.Password}");
            //Extract email address and password
            var email = request.Email;
            var password = request.Password;

            //validate data
            //password doesnt actually need validation given this is a login attempt, not a sign up.
            if( Utils.Validations.IsValidEmail(email) == false || Utils.Validations.IsValidPassword(password) == false)
            {
                Console.WriteLine($"AuthController.login Endpoint: Received data failed validation, returning error code 400");
                return BadRequest();
            }

            
            // call service: look for existing email
            var user = await _userService.FindUserViaEmail(email);
            if(user == null)
            {
                Console.WriteLine($"AuthController.login Endpoint: user not foud, returning 401");
                return Unauthorized();
            }
            else
            {
                Console.WriteLine($"AuthController.login Endpoint: user foud, passing password: {password} for hashing");
                
                var HashedPassword = HelperMethods.HashPassword(password);
                
                Console.WriteLine($"AuthController.login Endpoint: password has been hashed: {HashedPassword}, passing it for comparison");

                //for testing purposes, hashing will be disabled to 
                //bool Matching = true;
                bool Matching = HelperMethods.IsCorrectPassword(user.PasswordHash, HashedPassword);

                if (Matching)
                {
                    Console.WriteLine($"AuthController.login Endpoint: Correct password");
                    //create the tokens and return them
                    //create access token jwt
                        //store in memory
                        //should have userId and time it was signed and time it is to be expired
                        //the frontend checks if the token is expired, if it is, it sends the refresh instead
                        //if the token isnt expired its sent normally, if the token is expired on the backend though, a redirect link is sent to front end
                        //front end, after recieving redirect link, sends the refresh token
                        
                    // create refresh token string
                        //store in a cookie by a client
                        //in db by a server, should have the user Id, revoked or not, expiry time, expired or not!
                        //in the cases where the refresh token is sent it is checked to see if it is expired, revoked and if the user is the actual owner of the token, then a new access token is given and a new refresh token is given, existing refresh token is killed off
                        //refresh token is deleted when user logs out
                        //


                    //both are sent to client

                    
            

                    
                    return Ok();
                }else
                {
                    Console.WriteLine($"AuthController.login Endpoint: Incorrect password");
                    return Unauthorized();
                }
                
            } 


            


        }


//1- register user
//post request comes in
//contains password, email, username
//check if email exists, if yes, send error and redirect to log in page
//if no, validate all data is correct, then
//  create a user using email, username, hash password
//issue 2 tokens



        

//3- refresh token
//post comes in, going to have acces token and refresh token, if refresh token is valid, recreate an access token and send it back

//4- log out
//post request comes, with email
//check and verify access token, if the email matches the id present in the token, revoke that particular token
//if not, send an error message





    }

}