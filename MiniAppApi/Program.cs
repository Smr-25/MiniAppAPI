using Microsoft.EntityFrameworkCore;
using MiniAppApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var root = builder.Environment.ContentRootPath;
var env = "Code";
//root.JsonCreater(env);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{env}.json",
        optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
//Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddServices(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
