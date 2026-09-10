namespace backEnd.Models;

using System.ComponentModel.DataAnnotations;

//standard user
//if there was different types of log in then we would have to split the user to account details and user details but that's unnecessary for now
public class User
{
    public Guid Id {get; set;}
    [Required]
    public string UserName {get; set;} = string.Empty;
    [Required]
    public string Email {get; set;} = string.Empty;
    [Required]
    public string PasswordHash {get; set;} = string.Empty;
    [Required]
    public string DisplayName {get; set;} = string.Empty;
    public string? Bio {get; set;} 
    public string? AvatarStorageKey {get; set;}
    public DateTimeOffset CreatedAt {get; set;}
    public DateTimeOffset? UpdatedAt {get; set;}
    public DateTimeOffset? DisabledAt {get; set;}
    
}