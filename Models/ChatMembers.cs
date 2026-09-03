namespace backEnd.Models;

public class ChatMembers
{
    public Guid ChatId {get; set;}
    public Guid UserId {get; set;}
    public string Role {get; set;} = string.Empty;

    public DateTimeOffset JoinedAt {get; set;}

    public DateTimeOffset LeftAt {get; set;}
    
    public DateTimeOffset LastReadMessageId {get; set;}
    public DateTimeOffset LastReadAt {get; set;}

    public DateTimeOffset SuspendedUntil {get; set;}

    public bool IsPinned {get; set;} = false;

    

}