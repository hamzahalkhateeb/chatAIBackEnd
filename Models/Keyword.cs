using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backEnd.Models;

public class Keyword
{
    public Guid Id {get; set;}
    public Guid CategoryId {get; set;}
    public string Term {get; set;}=string.Empty;
    public DateTimeOffset CreatedAt {get; set;}
    
}