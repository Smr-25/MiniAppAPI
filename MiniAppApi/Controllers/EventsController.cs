using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Dtos.Event;
using MiniAppApi.Services;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(EventService eventService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var events = await eventService.GetAllEventsAsync();
        return Ok(events);
    }

    [HttpPost]
    public async Task<IActionResult> Post(EventCreateDto eventCreateDto)
    {
        await eventService.CreateEventAsync(eventCreateDto);
        return Ok();
    }
    
    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadEventImage(int id, IFormFile file)
    {
        await eventService.UploadEventImageAsync(id, file);
        return Ok();
    }

    [HttpGet("{eventId}/tickets")]
    public async Task<IActionResult> GetEventTickets(int eventId)
    {
        var tickets = await eventService.GetEventTicketsAsync(eventId);
        return Ok(tickets);
    }

    [HttpGet("{organizerId}/organizer")]
    public async Task<IActionResult> GetOrganizerOfEvent(int organizerId)
    {
        var organizer = await eventService.GetOrganizerOfEventAsync(organizerId);
        return Ok(organizer);
    }
}
