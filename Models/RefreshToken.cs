namespace backEnd.Models;

//refresh token, seperate from access token

public class RefreshToken
{
    public Guid Id {get; set;}
    public Guid UserId {get; set;}

    public string TokenHash {get; set;} = string.Empty;

    public DateTimeOffset ExpiresAt {get; set;}

    public DateTimeOffset  RevokedAt {get; set;}
    public DateTimeOffset  CreatedAt {get; set;}

    public string ReplacedByTokenId {get; set;}= string.Empty;
}