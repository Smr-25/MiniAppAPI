using AppSettingsMultiPlatformPackage;
using Microsoft.EntityFrameworkCore;
using MiniAppApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var root = builder.Environment.ContentRootPath;
var env = "Mac";
// root.JsonCreater(env);
//Add services to the container.
var configuration = builder.Configuration;
var configurationBuilder = new ConfigurationBuilder();
   
builder.Services.AddServices(configuration);
configurationBuilder.AddAppSettingsMultiPlatformJson(env);

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
