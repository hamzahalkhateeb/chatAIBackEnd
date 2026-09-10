using Microsoft.AspNetCore.Mvc;
using backEnd.Modules;
using backEnd.Modules.Authentication;
using backEnd.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using backEnd.Modules.Utils;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;

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

        public static bool IsCorrectPassword( string UserPassword, string ProvidedHashedPassword)
        {
            Console.WriteLine($"HelperMethods.IsCorrectPassword: method called, about to compare {UserPassword} and {ProvidedHashedPassword}");
            var result = _passwordHasher.VerifyHashedPassword(null!, UserPassword, ProvidedHashedPassword);
            return result == PasswordVerificationResult.Success;
        }

        public static string GenerateAccessToken(string UserId)
        {
            
            return UserId;
        }


    }
}