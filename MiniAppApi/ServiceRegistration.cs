using Microsoft.EntityFrameworkCore;
using MiniAppApi.Data;
using MiniAppApi.Profiles;

namespace MiniAppApi;

public static class ServiceRegistration 
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();
        services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddAutoMapper(opt =>
        {
            opt.AddProfile<MapProfile>();
        });
        services.AddScoped<Services.EventService>();


    }
}
