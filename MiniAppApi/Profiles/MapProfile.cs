using AutoMapper;
using MiniAppApi.Dtos;
using MiniAppApi.Dtos.Event;
using MiniAppApi.Dtos.Organizer;
using MiniAppApi.Dtos.Tickets;
using MiniAppApi.Models;

namespace MiniAppApi.Profiles;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Event, EventReturnDto>();
        CreateMap<EventCreateDto, Event>();
        CreateMap<Organizer, OrganizerReturnDto>();
        CreateMap<OrganizerCreateDto, Organizer>();
        CreateMap<TicketCreateDto, Ticket>();
        CreateMap<Ticket, TicketReturnDto>();

    }
}
