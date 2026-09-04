

namespace backEnd.Models;

public class MessageReaction
{
    public Guid MessageId {get; set;}
    public Guid SenderId {get; set;}

    public string Emoji {get; set;} = string.Empty;

    public DateTimeOffset CreatedAt {get; set;}
    
}