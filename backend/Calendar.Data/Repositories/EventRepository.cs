using Calendar.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Calendar.Data.Repositories;

public class EventRepository : IEventRepository
{
    private readonly CalendarContext _context;

    public EventRepository(CalendarContext context)
    {
        _context = context;
    }

    public async Task<Event> AddAsync(Event @event)
    {
        this._context.Events.Add(@event);

        await this._context.SaveChangesAsync();
        return @event;
    }

    public async Task<Event?> AddNotificationAsync(int id, Notification notification)
    {
        var @event = this._context.Events.Include(n => n.Notifications).FirstOrDefault(e => e.Id == id);
        if (@event == null)
        {
            return null;
        }

        notification.EventId = @event.Id;
        @event.Notifications.Add(notification);

        await this._context.SaveChangesAsync();
        return @event;
    }

    public async Task<bool> DeleteAsync(int userId)
    {
        var eventToDelete = await this.GetByIdAsync(userId);
        if (eventToDelete == null)
        {
            return false;
        }

        this._context.Events.Remove(eventToDelete);
        await this._context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Event>> GetByDateAsync(int userId, DateTime date)
    {
        return await this._context.Events
            .Include(n => n.Notifications)
            .Include(c => c.TodoItem)
            .Where(e => e.UserId == userId && e.StartAt == date.Date)
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await this._context.Events
            .Include(n => n.Notifications)
            .Include(c => c.TodoItem)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Event>> GetTodayEventsAsync(int userId)
    {
        return await this._context.Events
            .Include(n => n.Notifications)
            .Include(c => c.TodoItem)
            .Where(e => e.UserId == userId && e.StartAt == DateTime.Today)
            .ToListAsync();
    }

    public async Task<List<Event>> GetWeekEventsAsync(int userId, DateTime date)
    {
        return await this._context.Events
            .Include(n => n.Notifications)
            .Include(c => c.TodoItem)
            .Where(e => e.UserId == userId && e.StartAt >= date.Date && e.StartAt <= date.Date.AddDays(7))
            .ToListAsync();
    }

    public async Task<Event?> UpdateAsync(Event @event)
    {
        var eventToUpdate = this.GetByIdAsync(@event.Id);
        if (eventToUpdate == null)
        {
            return null;
        }

        this._context.Events.Update(@event);
        await this._context.SaveChangesAsync();

        return @event;
    }
}
