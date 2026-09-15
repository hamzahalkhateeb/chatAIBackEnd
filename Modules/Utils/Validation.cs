
using System.Text.RegularExpressions;

namespace backEnd.Modules.Utils
{



    public static class Validations
    {


        //validate an email address
        public static bool IsValidEmail(string email)
        {
            if (EnvConfig.IsDebuggingLogging)
                Console.WriteLine($"Utils.Validations.IsValidEmail method called, validation email: {email}");
            //if email is null or contains just white space
            if (string.IsNullOrWhiteSpace(email))
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidEmail: email: {email} is null or white space");
                return false;
            }

            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidEmail: email: {email} format validated successfully");
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            else
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidEmail: email: {email} email is invalid");
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }




        }

        //validate password
        public static bool IsValidPassword(string password)
        {
            if (EnvConfig.IsDebuggingLogging)
                Console.WriteLine($"Utils.Validations.IsValidPassword method called, validation password: {password}");
            if (string.IsNullOrWhiteSpace(password))
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidPassword: validation password: {password} is null or whitespace");
                return false;
            }


            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?"":{}|<>]).{8,}$";

            if (Regex.IsMatch(password, pattern))
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidPassword: validation password: {password} format validated successfully");
                return Regex.IsMatch(password, pattern);

            }
            else
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidPassword: validation password: {password} is invalid");
                return Regex.IsMatch(password, pattern);
            }


        }

        //validate username

        public static bool IsValidUsername(string username)
        {
            if (EnvConfig.IsDebuggingLogging)
                Console.WriteLine($"Utils.Validations.IsValidUsername method called, validating username: {username}");
            if (string.IsNullOrWhiteSpace(username))
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidUsername: username is null or whitespace");
                return false;
            }

            // 3-20 chars, letters/numbers/underscores only, must start with a letter
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_]{2,14}$";

            if (Regex.IsMatch(username, pattern))
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidUsername: username: {username} format validated successfully");
                return true;
            }
            else
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidUsername: username: {username} is invalid");
                return false;

            }
        }

        public static bool IsValidBio(string? bio)
        {
            if (EnvConfig.IsDebuggingLogging)
                Console.WriteLine($"Utils.Validations.IsValidBio method called, validating bio: {bio}");

            // Bio is optional, so null/empty is valid
            if (string.IsNullOrWhiteSpace(bio))
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidBio: bio is null or empty, treated as valid (optional field)");
                return true;
            }

            // Cap at 160 chars - long enough for a real bio, short enough to stay skimmable in a profile card
            const int maxBioLength = 160;

            if (bio.Length <= maxBioLength)
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidBio: bio: {bio} length validated successfully");
                return true;
            }
            else
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidBio: bio: {bio} exceeds max length of {maxBioLength}");
                return false;
            }
        }

        // validate displayname
        public static bool IsValidDisplayName(string displayName)
        {
            if (EnvConfig.IsDebuggingLogging)
                Console.WriteLine($"Utils.Validations.IsValidDisplayName method called, validating display name: {displayName}");

            if (string.IsNullOrWhiteSpace(displayName))
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidDisplayName: display name is null or whitespace");
                return false;
            }

            // 2-24 chars: short enough to fit in a chat UI without truncating/wrapping awkwardly,
            // long enough to allow full first+last names or short phrases
            const int minLength = 2;
            const int maxLength = 24;

            if (displayName.Length >= minLength && displayName.Length <= maxLength)
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidDisplayName: display name: {displayName} length validated successfully");
                return true;
            }
            else
            {
                if (EnvConfig.IsDebuggingLogging)
                    Console.WriteLine($"Utils.Validations.IsValidDisplayName: display name: {displayName} length is invalid (must be {minLength}-{maxLength} chars)");
                return false;
            }



        }
    }
}