using Microsoft.EntityFrameworkCore;

namespace Calendar.Models.Entities;

public class CalendarContext : DbContext
{
    public CalendarContext(DbContextOptions<CalendarContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>()
            .HasMany(c => c.Categories)
            .WithMany(t => t.TodoItems)
            .UsingEntity("TodoItemCategory");

    }
}
