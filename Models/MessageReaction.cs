

namespace backEnd.Models;

//entirely seperate table to have reactions to messages, allows for indexation of the message to retrieve all reactions related to it. i think this performs better and simplifies models

public class MessageReaction
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }

    public string Emoji { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

}