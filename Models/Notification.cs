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
    [Required]
    public DateTimeOffset CreatedAt {get; set;}
    [Required]
    public DateTimeOffset ReadAt {get; set;}

}