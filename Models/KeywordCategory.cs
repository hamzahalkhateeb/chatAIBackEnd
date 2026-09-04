namespace backEnd.Models;

//catrogires which the front end will be programmed to handle, potential categories will be celebration, naughty, super naughty
//categories here will be used to tag messages - if a word is scanned to match a key word, the message will be tagged with one of those categories and a message flag will be created
public class KeywordCategory
{
    public Guid Id {get; set;}
    public string Name {get; set;}=string.Empty;
    public string Severity {get; set;}=string.Empty;

    public DateTimeOffset CreatedAt {get; set;}
}