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
builder.Services.AddControllers();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));


builder.Services.AddEndpointsApiExplorer();

// This method records Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });

    // Configuramos cómo se ve el botón "Authorize" en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Poné tu token así: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Obligamos a que Swagger use esta seguridad por default
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var jwtSection = builder.Configuration.GetSection("JWT");
var secretKey = jwtSection["Key"];


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Activamos autorización en general
builder.Services.AddAuthorization();


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
