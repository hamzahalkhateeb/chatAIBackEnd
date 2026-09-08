
using System.Text.RegularExpressions;

namespace backEnd.Modules.Utils
{



    public static class Validations
    {


        //validate an email address
        public static bool IsValidEmail(string email)
        {
            //if email is null or contains just white space
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);


        }

        //validate password
        public static bool IsValidPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                return false;

            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?"":{}|<>]).{8,}$";

            return Regex.IsMatch(password, pattern);
        }
    }
}