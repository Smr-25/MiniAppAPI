using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Event;
using MiniAppApi.Dtos.Organizer;
using MiniAppApi.Models;
using MiniAppApi.Utils;

namespace MiniAppApi.Services;

public class EventService(AppDbContext dbContext, IMapper mapper, FileManager fileManager)
{
    public async Task<List<EventReturnDto>> GetAllEventsAsync()
    {
        var events = await dbContext.Events.Include(e => e.Organizer).Include(e=>e.Tickets).ToListAsync();
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

    public async Task UploadEventImageAsync(int eventId, IFormFile file)
    {
        var @event = await dbContext.Events.FindAsync(eventId);
        if (@event == null)
            throw new Exception("Event not found");
        var path = await fileManager.SaveEventBannerAsync(file);
        @event.BannerImageUrl = path;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<List<TicketReturnDto>> GetEventTicketsAsync(int eventId)
    {
        var @event = await dbContext.Events
            .Include(e => e.Tickets)
            .FirstOrDefaultAsync(e => e.Id == eventId);
        if (@event == null)
            throw new Exception("Event not found");
        
        var ticketDtos = mapper.Map<List<TicketReturnDto>>(@event.Tickets);
        return ticketDtos;
    }

    public async Task<OrganizerReturnDto> GetOrganizerOfEventAsync(int id)
    {
        var @event = await dbContext.Events
            .Include(e => e.Organizer)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (@event == null)
            throw new Exception("Event not found");
        var organizerDto = mapper.Map<OrganizerReturnDto>(@event.Organizer);
        return organizerDto;
    }
    
    
}