using AutoMapper;
using MiniAppApi.Dtos.Event;
using MiniAppApi.Models;

namespace MiniAppApi.Profiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Event, EventReturnDto>();
        CreateMap<EventCreateDto, Event>();

    }
}
