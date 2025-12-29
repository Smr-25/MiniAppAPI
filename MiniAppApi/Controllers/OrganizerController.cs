using Microsoft.AspNetCore.Mvc;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizerController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
       
    }
}
