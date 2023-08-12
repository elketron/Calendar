using Calendar.Models.Entities;

namespace Calendar.Data.Repositories;

public interface IEventRepository
{
    Task<Event?> GetByIdAsync(int id);
    Task<List<Event>> GetTodayEventsAsync(int userId);
    Task<List<Event>> GetByDateAsync(int userId, DateTime date);
    Task<List<Event>> GetWeekEventsAsync(int userId, DateTime date);
    Task<Event?> UpdateAsync(Event @event);
    Task<bool> DeleteAsync(int userId);
    Task<Event> AddAsync(Event @event);
    Task<Event> AddNotificationAsync(int id, Notification notification);
}
