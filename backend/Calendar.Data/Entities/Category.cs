
using System.ComponentModel.DataAnnotations;

namespace Calendar.Models.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<TodoItem> TodoItems { get; set; }
}

