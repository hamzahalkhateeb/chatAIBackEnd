namespace backEnd.Modules.Utils;

using Microsoft.AspNetCore.Http;
//used to store data retrieved from a found user
//example a user logs in, the fields listed below will be taken, the rest of the user data will be ignored
public class FoundUserDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;

}

//cookie settings to be reused in login, register and refresh token
public static class CookieHelper
{
    public static CookieOptions RefreshTokenCookieOptions() => new CookieOptions
    {
        //make the cookie inaccessible via code
        HttpOnly = true,
        //make the cookie only sendable via https
        Secure = true,
        //
        SameSite = SameSiteMode.Lax,
        Expires = DateTimeOffset.UtcNow.AddDays(7),
        Path = "/auth"

    };
}