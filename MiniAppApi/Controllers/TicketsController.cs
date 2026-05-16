using Microsoft.AspNetCore.Mvc;
using MiniAppApi.Dtos.Tickets;
using MiniAppApi.Services;

namespace MiniAppApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController(TicketService ticketService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tickets = await ticketService.GetAllTicketsAsync();
        return Ok(tickets);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TicketCreateDto ticketCreateDto)
    {
        await ticketService.CreateTicketAsync(ticketCreateDto);
        return Ok();
    }
}