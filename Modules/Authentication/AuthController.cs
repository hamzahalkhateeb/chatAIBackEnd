using Microsoft.AspNetCore.Mvc;
using backEnd.Modules;
using backEnd.Modules.Authentication;
using backEnd.Modules.Utils;
using backEnd.Modules.User;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Identity.Data;
using System.Text.Json;


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


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            Console.WriteLine($"AuthController.loginEndpoint: endpoint reached, attempted log in with email: {request.Email}, password: {request.Password}");
            //Extract email address and password
            var email = request.Email;
            var password = request.Password;
            //validate data
            //password doesnt actually need validation given this is a login attempt, not a sign up.
            if (Utils.Validations.IsValidEmail(email) == false)
            {
                Console.WriteLine($"AuthController.login Endpoint: Received data failed validation, returning error code 400");
                return BadRequest();
            }
            // call service: look for existing email
            var user = await _userService.FindUserViaEmail(email);
            if (user == null)
            {
                Console.WriteLine($"AuthController.login Endpoint: user not foud, returning 401");
                return Unauthorized(new { message = "Null User" });
            }
            else
            {
                Console.WriteLine($"AuthController.login Endpoint: user foud, passing password: {password} for hashing");
                ///delete password after register endpoint is complete
                var HashedPassword = HelperMethods.HashPassword(password);
                Console.WriteLine($"AuthController.login Endpoint: password has been hashed: {HashedPassword}, passing it for comparison");
                //for testing purposes, hashing will be disabled to 
                //bool Matching = true;
                bool Matching = HelperMethods.IsCorrectPassword(user.PasswordHash, HashedPassword);
                if (Matching)
                {
                    Console.WriteLine($"AuthController.login Endpoint: Correct password");
                    //create the tokens and return them
                    var AccessToken = HelperMethods.GenerateAccessToken(user.Id);
                    // create refresh token string
                    var RefreshToken = await _authService.GenerateRefreshToken(user.Id);
                    //append cookie
                    Response.Cookies.Append("RefreshToken", RefreshToken, CookieHelper.RefreshTokenCookieOptions());
                    return Ok(new { message = "Log in Successful", AccessToken });
                }
                else
                {
                    Console.WriteLine($"AuthController.login Endpoint: Incorrect password");
                    return Unauthorized(new { message = "Invalid Email or Password" });
                }
            }

        }

        [HttpPost("test")]
        public async Task<IActionResult> Test()
        {
            Console.WriteLine("AuthController.Test: endpoint reached");

            // 1. Read access token from Authorization header
            string? authHeader = Request.Headers["Authorization"];
            string? accessToken = null;

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                accessToken = authHeader["Bearer ".Length..].Trim();
            }

            Console.WriteLine($"AuthController.Test: access token received: {accessToken ?? "NONE"}");

            // 2. Read refresh token from cookie
            Request.Cookies.TryGetValue("refreshToken", out var refreshToken);

            Console.WriteLine($"AuthController.Test: refresh token received: {refreshToken ?? "NONE"}");

            return Ok(new
            {
                message = "Test endpoint reached",
                accessTokenReceived = accessToken != null,
                accessTokenValue = accessToken,
                refreshTokenReceived = refreshToken != null,
                refreshTokenValue = refreshToken
            });
        }


        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] JsonElement user)
        {
            //validate data like the following
            //email, username, password, display name, bio, avatar storage key - util.validations
            Console.WriteLine($"AuthController.Signup: endpoint reached, extracting data...");

            string UserName = body.GetProperty("UserName").GetString();
            string DisplayName = body.GetProperty("DisplayName").GetString();
            string Email = body.GetProperty("Email").GetString();
            string Password = body.GetProperty("Password").GetString();
            string? Bio = body.GetProperty("Bio").GetString();
            string? AvatarStorageKey = body.GetProperty("AvatarStorageKey").GetString();
            

            //check if user with same email or user name already exists, if already exists return error /user services

            //auto generate created at, updated and disabled at is null at first
            //hash password

            //save user using a auth service, use transaction

            //generate access token and refresh token and return

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