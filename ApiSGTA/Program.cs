using System.Reflection;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApiSGTA.Services;
using Infrastructure.UnitOfWork;
using Application.Interfaces;
using Microsoft.OpenApi.Models;

using ApiSGTA.Extensions;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());


// This method records Swagger
builder.Services.AddSwaggerDocumentation();




// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

builder.Services.AddDbContext<AutoTallerDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    options.UseNpgsql(connectionString);
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


// Servicios de aplicacion
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// This method records Rate Limiting
builder.Services.AddGlobalRateLimiting();

// This method records JWT Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);



var app = builder.Build();

app.UseHttpsRedirection();

// Checks if the application is running in the environment
if (app.Environment.IsDevelopment())
{
    // Starts JSON generation of Swagger 
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SGTA API V1");
        // To load the root, as an example: “https://localhost:5000/”
        options.RoutePrefix = string.Empty;
    });
}

// Necessary to use JWT
// First
app.UseAuthentication();
// Second
app.UseAuthorization();

// Necessary to use Rate Limiter
app.UseRateLimiter();

app.Run();
