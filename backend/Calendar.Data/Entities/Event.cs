using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar.Models.Entities;

public class Event
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsDone { get; set; }
    public string FileLink { get; set; } = string.Empty;
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public bool IsAllDay { get; set; }
    public bool IsRecurring { get; set; }
    public string RecurrenceRule { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public int? TodoItemId { get; set; }
    public TodoItem? TodoItem { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

