using Calendar.Models.Entities;
using Calendar.Data.Repositories;

namespace CalendarServices.Services;

public class EventsService
{
    private readonly IEventRepository _eventsRepository;

    public EventsService(IEventRepository eventsRepository)
    {
        _eventsRepository = eventsRepository;
    }

    public async Task<Event> Create(Event @event)
    {
        await _eventsRepository.AddAsync(@event);
        return @event;
    }

    public async Task<Event> Update(Event @event)
    {
        await _eventsRepository.UpdateAsync(@event);
        return @event;
    }

    public async Task<bool> Delete(int id)
    {
        return await _eventsRepository.DeleteAsync(id);
    }

    public async Task<Event?> Get(int id)
    {
        return await _eventsRepository.GetByIdAsync(id);
    }

    public async Task<List<Event>> GetToday(int userId)
    {
        return await _eventsRepository.GetTodayEventsAsync(userId);
    }

    public async Task<List<Event>> GetWeek(int userId, DateTime date)
    {
        return await _eventsRepository.GetWeekEventsAsync(userId, date);
    }

    public async Task<bool> AddNotification(int id, Notification notification)
    {
        return await _eventsRepository.AddNotificationAsync(id, notification) != null;
    }

    public async Task<bool> RemoveNotification(int id, int notificationId)
    {
        var @event = await _eventsRepository.GetByIdAsync(id);
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
        await _eventsRepository.UpdateAsync(@event);
        return true;
    }
}
