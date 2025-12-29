using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Models;

namespace MiniAppApi.Services;

public class EventService(AppDbContext dbContext)
{
    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await dbContext.Events.ToListAsync();
    }
    public async Task<Event?> GetEventByIdAsync(int id)
    {
        return await dbContext.Events.FindAsync(id);
    }
}
