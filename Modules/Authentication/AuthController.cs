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


namespace backEnd.Modules.Authentication
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly UserService _userService;
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        //dependancy inject services from other files
        //normal classes have to be injected here while static classes can be used directly 
        public AuthController(AuthService authService, UserService userService, IConfiguration config, AppDbContext db)
        {
            _authService = authService;
            _userService = userService;
            _db = db;
            _config = config;
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

            string UserName = user.TryGetProperty("UserName").GetString();
            string DisplayName = user.GetProperty("DisplayName").GetString();
            string Email = user.GetProperty("Email").GetString();
            string Password = user.GetProperty("Password").GetString();
            string? Bio = user.GetProperty("Bio").GetString();
            string? AvatarStorageKey = user.GetProperty("AvatarStorageKey").GetString();
            //following data is created now, it doesnt come from the request



            Console.WriteLine($"AuthController.Signup: Request Data Extracted: {user}");

            //check if user with same email or user name already exists, if already exists return error /user services
            var FoundUserWithEmail = await _userService.FindUserViaEmail(Email);
            var FoundUserWithUserName = await _userService.FindUserViaUserName(UserName);
            if (FoundUserWithEmail != null || FoundUserWithUserName != null)
            {
                return Conflict(new { message = "Email or Username already exists!" });
            }


            //validate data
            if (!Utils.Validations.IsValidEmail(Email) ||
                !Utils.Validations.IsValidPassword(Password) ||
                !Utils.Validations.IsValidUsername(UserName) ||
                !Utils.Validations.IsValidDisplayName(DisplayName) ||
                !Utils.Validations.IsValidBio(Bio))
            {
                Console.WriteLine($"AuthController.Signup: some data is corrupt, returning error bad request");
                return BadRequest();
            }


            //auto generate created at, updated and disabled at is null at first

            //hash password
            Console.WriteLine($"AuthController.Signup: hashing password");
            var HashedPassword = Utils.HelperMethods.HashPassword(Password);

            //save user using a auth service, use transaction when using the image uploader down the line, currently, it doesnt need a transactiona s its one action
            var newUser = new Models.User
            {
                UserName = UserName,
                Email = Email,
                PasswordHash = HashedPassword,
                DisplayName = DisplayName,
                Bio = Bio,
                AvatarStorageKey = AvatarStorageKey,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                DisabledAt = null
            };

            Console.WriteLine($"AuthController.Signup: about to save user in db - Email: {Email}, hashed password: {HashedPassword}, userName: {UserName}, display name: {DisplayName}, bio: {Bio}. CreatedAt, UpdatedAt and DisabledAt are auto generated");

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();

            Console.WriteLine($"AuthController.Signup: user saved successfully, assigning tokens");


            var AccessToken = HelperMethods.GenerateAccessToken(newUser.Id);

            var RefreshToken = await _authService.GenerateRefreshToken(newUser.Id);
            //append cookie
            Response.Cookies.Append("RefreshToken", RefreshToken, CookieHelper.RefreshTokenCookieOptions());
            return Ok(new { message = "signing up successful", AccessToken });



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