using MiniAppApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddServices(configuration, builder);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.MapScalarApiReference();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();