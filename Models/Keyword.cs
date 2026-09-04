

namespace backEnd.Models;

//key words will be stored for matching, for example slurs, swear words, congratulatory words, each message will be scanned to check if it matches any key words
//each word will also be assigned a category.
//if a message is scanned to have a keyword, a message flag will be created with the details and saved and send with the message everytime its downloaded 
public class Keyword
{
    public Guid Id {get; set;}
    public Guid CategoryId {get; set;}
    public string Term {get; set;}=string.Empty;
    public DateTimeOffset CreatedAt {get; set;}

}