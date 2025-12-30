using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Tickets;
using MiniAppApi.Models;

namespace MiniAppApi.Services;

public class TicketService(AppDbContext dbContext,IMapper mapper)
{
    public async Task<List<TicketReturnDto>> GetAllTicketsAsync()
    {
        var tickets = await dbContext.Tickets.ToListAsync();
        var ticketDtos = mapper.Map<List<TicketReturnDto>>(tickets);
        return ticketDtos;
    }
    
    public async Task CreateTicketAsync(TicketCreateDto ticketCreateDto)
    {
        var ticket = mapper.Map<Ticket>(ticketCreateDto);
        await dbContext.Tickets.AddAsync(ticket);
        await dbContext.SaveChangesAsync();
    }
}