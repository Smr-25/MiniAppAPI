using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Dtos.Organizers;
using MiniAppApi.Services;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizersController(OrganizerService organizerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var organizers = await organizerService.GetAllOrganizersAsync();
        return Ok(organizers);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrganizerCreateDto organizerCreateDto)
    {
        await organizerService.CreateOrganizerAsync(organizerCreateDto);
        return Ok();
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadOrganizerLogoImage(int id, [FromForm] IFormFile file)
    {
        await organizerService.UploadOrganizerLogoImageAsync(id, file);
        return Ok();
    }

    [HttpGet("{organizerId}/events")]
    public async Task<IActionResult> GetOrganizerEvents(int organizerId)
    {
        var events = await organizerService.GetOrganizerEventsAsync(organizerId);
        return Ok(events);
    }
}