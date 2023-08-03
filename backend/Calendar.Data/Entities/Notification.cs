using System.ComponentModel.DataAnnotations;

namespace Calendar.Models.Entities;

public class Notification
{
    [Key]
    public int Id { get; set; }
    public DateTime NotifyAt { get; set; }

    public int? EventId { get; set; }
    public Event? Event { get; set; } = null!;
}
