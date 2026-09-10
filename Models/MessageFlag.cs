namespace backEnd.Models;
using System.ComponentModel.DataAnnotations;
//will get saved everytime a message is saved, which will save compute at the cost of storage, can be sent to front attached to message

public class MessageFlag
{
    public Guid Id {get; set;}
    [Required]
    public Guid CategoryId {get; set;}
    [Required]
    public string MatchedTerm {get; set;}=string.Empty;
    public DateTimeOffset CreatedAt {get; set;}
}