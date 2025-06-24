using System.Reflection;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApiSGTA.Services;
using Infrastructure.UnitOfWork;
using Application.Interfaces;
using Microsoft.OpenApi.Models;
using ApiSGTA.Extensions;
using ApiSGTA.Helpers;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
// New
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.AddApplicationServices();
builder.Services.AddCustomRateLimiter();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// This method records Swagger
builder.Services.AddSwaggerGen(c =>
{
    //SwaggerDoc define la documentación de la API, "v1" es el identificador de la versión de la API, OpenApiInfo contiene la información general de la API.
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Project SGTA",
        Version = "v1",
        Description = "API SGTA",
        Contact = new OpenApiContact
        {
            Name = "Grupo",
            Email = ""
        }
    });
});

builder.Services.AddDbContext<AutoTallerDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
    options.UseNpgsql(connectionString);
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger() Habilita el middleware que genera el documento JSON de la API
    app.UseSwagger();
    //app.UseSwaggerUI() Configura la interfaz de usuario de Swagger
    app.UseSwaggerUI(c =>
    {
        //SwaggerEndpoint especifica la ruta del documento JSON de Swagger, "Project EF API v1" nombre de la API
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project API SGTA");
        //Define la ruta base para acceder a la interfaz de usuario
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
