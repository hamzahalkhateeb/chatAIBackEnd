
using System.ComponentModel.DataAnnotations;
namespace backEnd.Models;

//entirely seperate table to have reactions to messages, allows for indexation of the message to retrieve all reactions related to it. i think this performs better and simplifies models

public class MessageReaction
{
    public Guid Id { get; set; }
    [Required]
    public Guid MessageId { get; set; }
    [Required]
    public Guid SenderId { get; set; }
    [Required]
    public string Emoji { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

}