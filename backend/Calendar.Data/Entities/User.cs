using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [JsonIgnore]
    public ICollection<Event> Events { get; set; } = new List<Event>();
    [JsonIgnore]
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    [JsonIgnore]
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
