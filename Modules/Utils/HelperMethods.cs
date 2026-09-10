using Microsoft.AspNetCore.Mvc;
using backEnd.Modules;
using backEnd.Modules.Authentication;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using backEnd.Modules.Utils;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using  System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
namespace backEnd.Modules.Utils
{
    public class HelperMethods
    {
        //create a password hashing code here
        //declare an instance of password hasher
        //please note the object being passed into this has no effect on anything.
        private static readonly PasswordHasher<object> _passwordHasher = new();


        public static string HashPassword(string PlainPassword)
        {
            Console.WriteLine($"HelperMethods.HashPassword: method called, about to hash a password: {PlainPassword}");
            //first variable passed has to be an object of any sort
            //an object is needed under the hood for this to work at all
            var HashedPassword = _passwordHasher.HashPassword(null!, PlainPassword);
            Console.WriteLine($"HelperMethods.HashPassword: password hashed from {PlainPassword} to {HashedPassword}");
            return HashedPassword;

        }

        //
        public static bool IsCorrectPassword(string UserPassword, string ProvidedHashedPassword)
        {
            Console.WriteLine($"HelperMethods.IsCorrectPassword: method called, about to compare {UserPassword} and {ProvidedHashedPassword}");
            var result = _passwordHasher.VerifyHashedPassword(null!, UserPassword, ProvidedHashedPassword);
            return result == PasswordVerificationResult.Success;
        }


        public static string GenerateSecureRandomString(int byteLength = 64)
        {
            var bytes = RandomNumberGenerator.GetBytes(byteLength);
            var secureString = Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");

            Console.WriteLine($"HelperMethods.GenerateSecureRandomStrIng: method called, Generated a secure 64 byte string: {secureString}");

            return secureString;
        }

        //different hashing algorithm than the password hasher
        public static string HashToken(string RawToken)
        {
            Console.WriteLine($"HelperMethods.HashToken: method called");
            var secret = Environment.GetEnvironmentVariable("REFRESH_SECRET") ?? throw new InvalidOperationException("REFRESH_SECRET_NOT_FOUND");
            //initilize a hmacsha256 object, pass it the secret
            //using the "using var" disposes of the variable as soon as its done with it for safety purposes
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            Console.WriteLine($"HelperMethods.HashToken: declared a hmac variable to hold an instance of HMACSHA256 object: {hmac}");

            //uses the method of the class to encode the raw token to binary code
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(RawToken));
            Console.WriteLine($"HelperMethods.HashToken: binary code for token: {hashBytes} and base 64 string version of token: {Convert.ToBase64String(hashBytes)}");

            //converts the binary code to a normal string
            return Convert.ToBase64String(hashBytes);

        }

        //the following method is in this file as it doesnt touch the db
        //refresh token is in services because it touches the db
        public static string GenerateAccessToken(string UserId)
        {
            //get secret
            var secret = Environment.GetEnvironmentVariable("ACCESS_SECRET")??throw new InvalidOperationException("SECRET not found");

            //things to attach to the token
            Console.WriteLine($"HelperMethods.GenerateAccessToken: method called, about to create claims");
            
            var claims = new[]
            {
                //jwtregistreted defines a small set of standard claim names
                //.sub stands for subject to note "who is this key for?" in this case its the user, hence user id
                new Claim(JwtRegisteredClaimNames.Sub, UserId.ToString()),
                //jti stands for jwt id, so we assign it a guid
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            Console.WriteLine($"HelperMethods.GenerateAccessToken: claims created: {claims}");

            //new instance of symmetric security key, it takes the env secret, however, it only takes bytes form of it, not direct strings            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            //signs the key and chooses which algorithm to incrypt it
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            Console.WriteLine($"HelperMethods.GenerateAccessToken: created key and credentials for the token - key: {key}, creds: {creds}");
            //finally, compose the full jwt
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );

            var AccessToken =  new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine($"HelperMethods.GenerateAccessToken: final Access Token: {AccessToken}");
            
            return AccessToken;
        }

        
    }
}