using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos.Event;
using MiniAppApi.Dtos.Organizer;
using MiniAppApi.Models;
using MiniAppApi.Utils;

namespace MiniAppApi.Services;

public class OrganizerService(AppDbContext dbContext,IMapper mapper, FileManager fileManager)
{
    public async Task<List<OrganizerReturnDto>> GetAllOrganizersAsync()
    {
        var organizer = await dbContext.Organizers.Include(o=>o.Events).ToListAsync();
        var organizerDto = mapper.Map<List<OrganizerReturnDto>>(organizer);
        return organizerDto;
    }
    
    public async Task CreateOrganizerAsync(OrganizerCreateDto organizerCreateDto)
    {
        var organizer = mapper.Map<Organizer>(organizerCreateDto);
        await dbContext.Organizers.AddAsync(organizer);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task UploadOrganizerLogoAsync(int organizerId, IFormFile file)
    {
        var organizer = await dbContext.Organizers.FindAsync(organizerId);
        if (organizer == null)
            throw new Exception("Organizer not found");
        var path = await fileManager.SaveOrganizerLogoAsync(file);
        organizer.LogoUrl = path;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<List<EventReturnDto>> GetOrganizerEventsAsync(int organizerId)
    {
        var organizer = await dbContext.Organizers
            .Include(o => o.Events)
            .FirstOrDefaultAsync(o => o.Id == organizerId);
        if (organizer == null)
            throw new Exception("Organizer not found");
        
        var eventDtos = mapper.Map<List<EventReturnDto>>(organizer.Events);
        return eventDtos;
    }
}
