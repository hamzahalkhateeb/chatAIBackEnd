namespace backEnd.Models;
using System.ComponentModel.DataAnnotations;


public class Chat
{
    public Guid Id {get; set;}
    
    [Required]
    public bool DirectChat {get; set;} = false;

    [Required]
    public string Name {get; set;} = string.Empty;

    [Required]
    public DateTimeOffset CreatedAt {get; set;}

    //created by
    [Required]
    public Guid CreatedBy {get; set;}
    //updated at
    public DateTimeOffset? UpdatedAt {get; set;}
    public Guid? PinnedMessageId {get; set;}

    public Guid? PinnedUser {get; set;}
    
}