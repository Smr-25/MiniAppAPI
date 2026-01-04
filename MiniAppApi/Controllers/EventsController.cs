using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Dtos.Events;
using MiniAppApi.Dtos.Tickets;
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
    public async Task<IActionResult> Post([FromBody] EventCreateDto eventCreateDto)
    {
        await eventService.CreateEventAsync(eventCreateDto);
        return Ok();
    }

    [HttpPost("{id}/banner")]
    public async Task<IActionResult> UploadEventBannerImage(int id, [FromForm] EventCreateBannerDto eventCreateBannerDto)
    {
        await eventService.UploadEventBannerImageAsync(id, eventCreateBannerDto);
        return Ok();
    }

    [HttpGet("{eventId}/tickets")]
    public async Task<IActionResult> GetEventTickets(int eventId)
    {
        var tickets = await eventService.GetEventTicketsAsync(eventId);
        return Ok(tickets);
    }

    [HttpGet("{eventId}/organizer")]
    public async Task<IActionResult> GetOrganizerOfEvent(int eventId)
    {
        var organizer = await eventService.GetOrganizerOfEventAsync(eventId);
        return Ok(organizer);
    }

    [HttpPost("{eventId}/tickets")]
    public async Task<IActionResult> CreateTicketForEvent(int eventId,
        [FromBody] TicketCreateByEventDto ticketCreateDto)
    {
        await eventService.CreateTicketForEventAsync(eventId, ticketCreateDto);
        return Ok();
    }
}