using backEnd.Data;
using backEnd.Modules.Utils;
using Microsoft.EntityFrameworkCore;

namespace backEnd.Modules.User;
public class UserService
{
    //dependancy inject db context 
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //look for an existing user with this email
    //if found return, id, username, display name, email
    //other details  like bio, etc will be loaded if user requests in another section

    public async Task<FoundUserDTO?> FindUserViaEmail(string email)
    {
        Console.WriteLine($"UserService.FindUserViaEmail: method called, finding user with email: {email}");
        var user = await _dbContext.Users.Where(u => u.Email == email)
        .Select(u => new FoundUserDTO
        {
            Id = u.Id,
            UserName = u.UserName,
            DisplayName = u.DisplayName,
            Email = u.Email,
            PasswordHash = u.PasswordHash
        })
        .FirstOrDefaultAsync();
        Console.WriteLine($"UserService.FindUserViaEmail: results found {user}");
        return user;
    }



    //create a new user using these details

}