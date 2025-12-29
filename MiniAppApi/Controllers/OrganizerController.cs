using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Services;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizerController(OrganizerService organizerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
       var organizers =  await organizerService.GetAllOrganizersAsync();
       return Ok(organizers);
    }
}
