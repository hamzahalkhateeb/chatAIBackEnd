namespace backEnd.Models;
using System.ComponentModel.DataAnnotations;

//catrogires which the front end will be programmed to handle, potential categories will be celebration, naughty, super naughty
//categories here will be used to tag messages - if a word is scanned to match a key word, the message will be tagged with one of those categories and a message flag will be created
public class KeywordCategory
{
    public Guid Id {get; set;}
    [Required]
    public string Name {get; set;}=string.Empty;
    [Required]
    public string Severity {get; set;}=string.Empty;
    [Required]
    public DateTimeOffset CreatedAt {get; set;}
}