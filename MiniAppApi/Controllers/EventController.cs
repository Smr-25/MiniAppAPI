using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Dtos.Event;
using MiniAppApi.Services;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController(EventService eventService) : ControllerBase
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

}
