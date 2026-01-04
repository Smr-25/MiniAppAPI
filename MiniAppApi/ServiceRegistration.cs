using AppSettingsMultiPlatformPackage;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Dtos.Organizers;
using MiniAppApi.Profiles;
using MiniAppApi.Services;
using MiniAppApi.Utils;

namespace MiniAppApi;

public static class ServiceRegistration 
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration,WebApplicationBuilder builder)
    {
        services.AddControllers();
        services.AddAppSettingsMultiPlatformJson(builder,"Mac");
        services.AddValidatorsFromAssemblyContaining<OrganizerCreateDtoValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddOpenApi();
        services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddAutoMapper(opt =>
        {
            opt.AddProfile<MapProfile>();
        });
       
        services.AddScoped<EventService>();
        services.AddScoped<OrganizerService>();
        services.AddScoped<TicketService>();
        services.AddScoped<FileManager>();


    }
}
