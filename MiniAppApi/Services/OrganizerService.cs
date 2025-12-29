using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos.Organizer;

namespace MiniAppApi.Services;

public class OrganizerService(AppDbContext dbContext,IMapper mapper)
{
    public async Task<OrganizerReturnDto> GetAllOrganizersAsync()
    {
        var organizer = await dbContext.Organizers.FirstOrDefaultAsync();
        var organizerDto = mapper.Map<OrganizerReturnDto>(organizer);
        return organizerDto;
    }
}
