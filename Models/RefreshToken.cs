namespace backEnd.Models;
using System.ComponentModel.DataAnnotations;

//refresh token, seperate from access token

public class RefreshToken
{
    public Guid Id {get; set;}
    [Required]
    public Guid UserId {get; set;}
    [Required]
    public string TokenHash {get; set;} = string.Empty;
    [Required]
    public DateTimeOffset ExpiresAt {get; set;}

    public DateTimeOffset?  RevokedAt {get; set;}
    [Required]
    public DateTimeOffset  CreatedAt {get; set;}

    public string? ReplacedByTokenId {get; set;}= string.Empty;
}