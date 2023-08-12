
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar.Models.Entities;

public enum Quadrant
{
    UrgentAndImportant = 1,
    NotUrgentAndImportant,
    UrgentAndNotImportant,
    NotUrgentAndNotImportant
}

public class TodoItem
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FileLink { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public Quadrant Quadrant { get; set; }
    public DateTime? DueAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Category> Categories { get; set; } = new List<Category>();

}

