using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
       
    }

    [HttpPost]
    public IActionResult Post()
    {

    }
}
