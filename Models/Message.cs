
namespace backEnd.Models;

public class Message
{
    public Guid Id {get; set;}

    public Guid ChatId {get; set;}

    public Guid SenderId {get; set;}

    public string Body {get; set;} = string.Empty;

    public Guid ReplyToMessageId {get; set;}

    public DateTimeOffset  CreatedAt {get; set;}

    public DateTimeOffset UpdatedAt {get; set;}
    public DateTimeOffset DeletedAt {get; set;}

    public bool IsEdited {get; set;} = false;
    public bool IsDeleted {get; set;} = false;

    public List<string> EditHistory {get; set;} = new();
    public Guid AttachmentId {get; set;}
}