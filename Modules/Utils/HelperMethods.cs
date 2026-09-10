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

        public static string GenerateAccessToken(string UserId)
        {

            return UserId;
        }

        
    }
}