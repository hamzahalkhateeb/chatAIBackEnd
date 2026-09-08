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

        public AuthController(AuthService authService)
        {
            _authService = authService;
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
            //Extract email address and password
            var email = request.Email;
            var password = request.Password;

            //validate data
            if( Utils.Validations.IsValidEmail(email) == false || Utils.Validations.IsValidPassword(password) == false)
            {
                return BadRequest();
            }

            
            //call service: look for existing email


            return Ok();


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