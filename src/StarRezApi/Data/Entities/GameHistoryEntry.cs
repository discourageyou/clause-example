using System.ComponentModel.DataAnnotations;

namespace StarRezApi.Data.Entities;

public class GameHistoryEntry
{
    [Key]
    public Guid Id { get; set; }

    public int KidNumber { get; set; }

    [Required]
    [MaxLength(100)]
    public string KidResponse { get; set; } = string.Empty;

    public DateTime ValidatedAt { get; set; }
}
