namespace backEnd.Models;

public class KeywordCategory
{
    public Guid Id {get; set;}
    public string Name {get; set;}=string.Empty;
    public string Severity {get; set;}=string.Empty;

    public DateTimeOffset CreatedAt {get; set;}
}