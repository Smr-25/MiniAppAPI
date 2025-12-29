using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos.Event;
using MiniAppApi.Models;

namespace MiniAppApi.Services;

public class EventService(AppDbContext dbContext, IMapper mapper)
{
    public async Task<List<EventReturnDto>> GetAllEventsAsync()
    {
        var events = await dbContext.Events.Include(e=>e.Organizer).ToListAsync();
        var eventsDto = mapper.Map<List<EventReturnDto>>(events);
        return eventsDto;
    }
    public async Task<EventReturnDto> GetEventByIdAsync(int id)
    {
        var @event = await dbContext.Events.FindAsync(id);
        var eventDto = mapper.Map<EventReturnDto>(@event);
        return eventDto;
    }

    public async Task CreateEventAsync(EventCreateDto eventCreateDto)
    {
        var @event = mapper.Map<Event>(eventCreateDto);
        dbContext.Events.Add(@event);
        await dbContext.SaveChangesAsync();
    }
}
