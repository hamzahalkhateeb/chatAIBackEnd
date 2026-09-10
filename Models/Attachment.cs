namespace backEnd.Models;
using System.ComponentModel.DataAnnotations;

public class Attachment
{
    public Guid Id { get; set; }

    [Required]
    public Guid MessageId { get; set; }

    public string? Name { get; set; } = string.Empty;
    public int? SizeBytes { get; set; }

    public string? StorageKey { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }

}