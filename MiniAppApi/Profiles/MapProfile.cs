using AutoMapper;
using MiniAppApi.Dtos.Events;
using MiniAppApi.Dtos.Organizers;
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
        CreateMap<TicketCreateByEventDto, Ticket>();
        CreateMap<Ticket, TicketReturnDto>();
    }
}