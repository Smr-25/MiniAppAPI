using Microsoft.EntityFrameworkCore;
using MiniAppApi.Models;

namespace MiniAppApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}
