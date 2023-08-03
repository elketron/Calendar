using Microsoft.AspNetCore.Mvc;
using CalendarServices.Services;
using Calendar.Models.Entities;

namespace Calendar.API.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly EventsService _eventService;

    public EventController(EventsService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<List<Event>> GetAll()
    {
        return await _eventService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<Event?> Get(int id)
    {
        return await _eventService.Get(id);
    }

    [HttpGet("today/{date}")]
    public async Task<List<Event>> GetToday(DateTime date)
    {
        return await _eventService.GetToday(date);
    }

    [HttpGet("week/{startDate}")]
    public async Task<List<Event>> GetWeek(DateTime startDate)
    {
        return await _eventService.GetWeek(startDate);
    }

    [HttpPost]
    public async Task<Event> Create(Event eventItem)
    {
        return await _eventService.Create(eventItem);
    }

    [HttpPut("")]
    public async Task<Event?> Update(Event eventItem)
    {
        return await _eventService.Update(eventItem);
    }

    [HttpPost("notification/{id}")]
    public async Task<bool> AddNotification(int id, DateTime date)
    {
        var notification = new Notification
        {
            NotifyAt = date
        };
        return await _eventService.AddNotification(id, notification);
}

[HttpDelete("{id}")]
public async Task<Event?> Delete(int id)
{
    return await _eventService.Delete(id);
}

[HttpDelete("notification/{id}/{notificationId}")]
public async Task<bool> DeleteNotification(int id, int notificationId)
{
    return await _eventService.RemoveNotification(id, notificationId);
}
}
