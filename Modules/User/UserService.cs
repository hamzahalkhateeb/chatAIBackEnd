using backEnd.Data;
using backEnd.Modules.Utils;
using Microsoft.EntityFrameworkCore;

namespace backEnd.Modules.User;

public class UserService
{
    //dependancy inject db context 
    private readonly AppDbContext _db;


    public UserService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    //look for an existing user with this email
    //if found return, id, username, display name, email
    //other details  like bio, etc will be loaded if user requests in another section

    public async Task<FoundUserDTO?> FindUserViaEmail(string email)
    {
        if (EnvConfig.IsDebuggingLogging)
            Console.WriteLine($"UserService.FindUserViaEmail: method called, finding user with email: {email}");
        var user = await _db.Users.Where(u => u.Email == email)
        .Select(u => new FoundUserDTO
        {
            Id = u.Id,
            UserName = u.UserName,
            DisplayName = u.DisplayName,
            Email = u.Email,
            PasswordHash = u.PasswordHash
        })
        .FirstOrDefaultAsync();
        if (EnvConfig.IsDebuggingLogging)
            Console.WriteLine($"UserService.FindUserViaEmail: results found {user}");
        return user;
    }
    //look for an exisstingg user with a usernaame
    public async Task<FoundUserDTO?> FindUserViaUserName(string UserName)
    {
        if (EnvConfig.IsDebuggingLogging)
            Console.WriteLine($"UserService.FindUserUserNNaaame: method called, finding user with UserName: {UserName}");
        var user = await _db.Users.Where(u => u.UserName == UserName)
        .Select(u => new FoundUserDTO
        {
            Id = u.Id,
            UserName = u.UserName,
            DisplayName = u.DisplayName,
            Email = u.Email,
            PasswordHash = u.PasswordHash
        })
        .FirstOrDefaultAsync();
        if (EnvConfig.IsDebuggingLogging)
            Console.WriteLine($"UserService.FindUserViaUserName: results found {user}");
        return user;
    }


    //create a new user using these details
    public async Task<Models.User?> SaveUser(Models.User user)
    {
        if (EnvConfig.IsDebuggingLogging)
            Console.WriteLine($"UserService.SaveUser: about to save user in db - Email: {user.Email}, userName: {user.UserName}, display name: {user.DisplayName}, bio: {user.Bio}");

        _db.Users.Add(user);
        var rowsSaved = await _db.SaveChangesAsync();

        if (rowsSaved <= 0)
        {
            if (EnvConfig.IsDebuggingLogging)
                Console.WriteLine($"UserService.SaveUser: SaveChangesAsync reported 0 rows affected, returning null");
            return null;

        }
        if (EnvConfig.IsDebuggingLogging)
            Console.WriteLine($"UserService.SaveUser: user saved successfully with Id: {user.Id}");
        return user;
    }
}