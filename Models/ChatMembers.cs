namespace backEnd.Models;
using System.ComponentModel.DataAnnotations;

public class ChatMembers
{
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid ChatId{get; set;}

    //admin, regular, second hand etc
    //sub admin can have less powers like deleting messages, maybe name this mod for the lols
    [Required]
    public string Role { get; set; } = string.Empty;

    [Required]
    public DateTimeOffset JoinedAt { get; set; }

    
    public DateTimeOffset? LeftAt { get; set; }

    public DateTimeOffset? LastReadMessageId { get; set; }
    public DateTimeOffset? LastReadAt { get; set; }

    public DateTimeOffset? SuspendedUntil { get; set; }





}