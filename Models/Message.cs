
namespace backEnd.Models;
using System.ComponentModel.DataAnnotations;

public class Message
{
    public Guid Id {get; set;}

    [Required]
    public Guid ChatId {get; set;}
    [Required]
    public Guid SenderId {get; set;}

    public string Body {get; set;} = string.Empty;

    public Guid? ReplyToMessageId {get; set;}

    [Required]
    public DateTimeOffset  CreatedAt {get; set;}
    
    public DateTimeOffset? UpdatedAt {get; set;}
    public DateTimeOffset? DeletedAt {get; set;}

    public bool IsEdited {get; set;} = false;
    public bool IsDeleted {get; set;} = false;

    public List<string>? EditHistory {get; set;} = new();
    public Guid? AttachmentId {get; set;}
}