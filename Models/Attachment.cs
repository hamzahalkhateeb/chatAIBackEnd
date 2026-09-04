namespace backEnd.Models;

public class Attachment
{
    public Guid Id { get; set; }

    public Guid MessageId { get; set; }

    public string Name { get; set; } = string.Empty;
    public int SizeBytes { get; set; }

    public string StorageKey { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }

}