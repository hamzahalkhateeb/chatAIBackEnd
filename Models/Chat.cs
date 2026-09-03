namespace backEnd.Models;

public class Chat
{
    public Guid Id {get; set;}
    public bool DirectChat {get; set;} = false;

    public string Name {get; set;} = string.Empty;

    //created at
    public DateTimeOffset CreatedAt {get; set;}

    //created by
    public Guid CreatedBy {get; set;}
    //updated at
    public DateTimeOffset UpdatedAt {get; set;}
    
}