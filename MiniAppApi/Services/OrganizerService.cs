using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos.Events;
using MiniAppApi.Dtos.Organizers;
using MiniAppApi.Exceptions;
using MiniAppApi.Models;
using MiniAppApi.Utils;

namespace MiniAppApi.Services;

public class OrganizerService(AppDbContext dbContext, IMapper mapper, FileManager fileManager)
{
    public async Task<List<OrganizerReturnDto>> GetAllOrganizersAsync()
    {
        var organizer = await dbContext.Organizers.Include(o => o.Events).ToListAsync();
        var organizerDto = mapper.Map<List<OrganizerReturnDto>>(organizer);
        return organizerDto;
    }
    
    public async Task CreateOrganizerAsync(OrganizerCreateDto organizerCreateDto)
    {
        var organizer = mapper.Map<Organizer>(organizerCreateDto);
        await dbContext.Organizers.AddAsync(organizer);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task UploadOrganizerLogoImageAsync(int organizerId, OrganizerCreateLogoDto organizerCreateLogoDto)
    {
        var organizer = await dbContext.Organizers.FindAsync(organizerId);
        if (organizer == null)
            throw new EntityNotFoundException(nameof(Organizer), organizerId);
        
        var path = await fileManager.SaveOrganizerLogoAsync(organizerId, organizerCreateLogoDto.LogoImage);
        organizer.LogoImageUrl = path;
        await dbContext.SaveChangesAsync();
    }
    
    public async Task<List<EventReturnDto>> GetOrganizerEventsAsync(int organizerId)
    {
        var organizer = await dbContext.Organizers
            .Include(o => o.Events).ThenInclude(e => e.Tickets)
            .FirstOrDefaultAsync(o => o.Id == organizerId);
        if (organizer == null)
            throw new EntityNotFoundException(nameof(Organizer), organizerId);
        
        var eventDto = mapper.Map<List<EventReturnDto>>(organizer.Events);
        return eventDto;
    }
}
