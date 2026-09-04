namespace backEnd.Models;

public class Notification
{
    public Guid Id {get; set;}
    public Guid ReceiverId {get; set;}
    public Guid ChatId {get; set;}
    public Guid MessageId {get; set;}
    public string Payload {get; set;}=string.Empty;
    public DateTimeOffset CreatedAt {get; set;}
    public DateTimeOffset ReadAt {get; set;}

}