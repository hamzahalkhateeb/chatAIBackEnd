namespace backEnd.Models;
using System.ComponentModel.DataAnnotations;


public class Notification
{
    public Guid Id {get; set;}
    [Required]
    public Guid ReceiverId {get; set;}
    [Required]
    public Guid ChatId {get; set;}
    [Required]
    public Guid MessageId {get; set;}
    public string Payload {get; set;}=string.Empty;
    public DateTimeOffset CreatedAt {get; set;}
    public DateTimeOffset ReadAt {get; set;}

}