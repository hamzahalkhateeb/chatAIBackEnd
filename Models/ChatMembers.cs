namespace backEnd.Models;

public class ChatMembers
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    //admin, regular, second hand etc
    //sub admin can have less powers like deleting messages, maybe name this mod for the lols
    public string Role { get; set; } = string.Empty;

    public DateTimeOffset JoinedAt { get; set; }

    public DateTimeOffset LeftAt { get; set; }

    public DateTimeOffset LastReadMessageId { get; set; }
    public DateTimeOffset LastReadAt { get; set; }

    public DateTimeOffset SuspendedUntil { get; set; }





}