using Calendar.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CalendarServices.Services;

public class EventsService
{
    private readonly CalendarContext _context;

    public EventsService(CalendarContext context)
    {
        _context = context;
    }

    public async Task<Event> Create(Event @event)
    {
        _context.Events.Add(@event);
        await _context.SaveChangesAsync();
        return @event;
    }

    public async Task<Event> Update(Event @event)
    {
        _context.Events.Update(@event);
        await _context.SaveChangesAsync();
        return @event;
    }

    public async Task<Event?> Delete(int id)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event != null)
        {
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
        }
        return @event;
    }

    public async Task<Event?> Get(int id)
    {
        return await _context.Events.Include(e => e.Notifications).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Event>> GetAll()
    {
        var events = await _context.Notifications.Include(n => n.Event).ToListAsync();

        return events.Select(e => e.Event).ToList();
    }
    public async Task<List<Event>> GetToday(DateTime date)
    {
        return await _context.Events.Include(e => e.Notifications).Where(t => t.StartAt >= date && t.StartAt <= date.AddDays(1)).ToListAsync();
    }

    public async Task<List<Event>> GetWeek(DateTime date)
    {
        return await _context.Events.Include(e => e.Notifications).Where(t => t.StartAt >= date && t.StartAt <= date.AddDays(7)).ToListAsync();
    }

    public async Task<bool> AddNotification(int id, Notification notification)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            return false;
        }
        @event.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveNotification(int id, int notificationId)
    {
        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            return false;
        }
        var notification = @event.Notifications.FirstOrDefault(n => n.Id == notificationId);
        if (notification == null)
        {
            return false;
        }
        @event.Notifications.Remove(notification);
        await _context.SaveChangesAsync();
        return true;
    }
}
