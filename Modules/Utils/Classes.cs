namespace backEnd.Modules.Utils;

//used to store data retrieved from a found user
//example a user logs in, the fields listed below will be taken, the rest of the user data will be ignored
public class FoundUserDTO {
    public Guid Id {get; set;}
    public string UserName {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
    public string DisplayName {get; set;} = string.Empty;
    
}