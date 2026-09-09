
using System.Text.RegularExpressions;

namespace backEnd.Modules.Utils
{



    public static class Validations
    {


        //validate an email address
        public static bool IsValidEmail(string email)
        {
            Console.WriteLine($"Utils.Validations.IsValidEmail method called, validation email: {email}");
            //if email is null or contains just white space
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine($"Utils.Validations.IsValidEmail: email: {email} is null or white space");
                return false;
            }

            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                Console.WriteLine($"Utils.Validations.IsValidEmail: email: {email} format validated successfully");
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            else
            {
                Console.WriteLine($"Utils.Validations.IsValidEmail: email: {email} email is invalid");
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }




        }

        //validate password
        public static bool IsValidPassword(string password)
        {
            Console.WriteLine($"Utils.Validations.IsValidPassword method called, validation password: {password}");
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine($"Utils.Validations.IsValidPassword: validation password: {password} is null or whitespace");
                return false;
            }
                

            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?"":{}|<>]).{8,}$";

            if(Regex.IsMatch(password, pattern))
            {
                Console.WriteLine($"Utils.Validations.IsValidPassword: validation password: {password} format validated successfully");
                return Regex.IsMatch(password, pattern);

            } else
            {
                Console.WriteLine($"Utils.Validations.IsValidPassword: validation password: {password} is invalid");
                return Regex.IsMatch(password, pattern);
            }

            
        }
    }
}