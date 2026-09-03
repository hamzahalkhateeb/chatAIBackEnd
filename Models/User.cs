namespace backEnd.Models;

public class User
{
    public Guid Id {get; set;}
    public string UserName {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
    public string DisplayName {get; set;} = string.Empty;
    public string? Bio {get; set;} = string.Empty;
    public string? AvatarStorageKey {get; set;}
    public DateTimeOffset CreatedAt {get; set;}
    public DateTimeOffset UpdatedAt {get; set;}
    public DateTimeOffset DisabledAt {get; set;}
    
}