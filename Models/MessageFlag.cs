namespace backEnd.Models;

public class MessageFlag
{
    public Guid Id {get; set;}
    public Guid CategoryId {get; set;}
    public string MatchedTerm {get; set;}=string.Empty;
    public DateTimeOffset CreatedAt {get; set;}
}