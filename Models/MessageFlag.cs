namespace backEnd.Models;

//will get saved everytime a message is saved, which will save compute at the cost of storage, can be sent to front attached to message

public class MessageFlag
{
    public Guid Id {get; set;}
    public Guid CategoryId {get; set;}
    public string MatchedTerm {get; set;}=string.Empty;
    public DateTimeOffset CreatedAt {get; set;}
}