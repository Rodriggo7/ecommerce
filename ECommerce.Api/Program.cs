using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ECommerce.Api.Middleware;
using ECommerce.Infrastructure;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
// 1. REGISTRO DE SERVICIOS (DI CONTENEDOR)
// Registrar dependencias de Infraestructura
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autorización JWT. Escribe 'Bearer {tu_token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});
// Registrar los servicios de la capa de Infraestructura (DbContext y Repositorios)
builder.Services.AddInfrastructureServices(builder.Configuration);
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
        };
    });

builder.Services.AddAuthorization(); // Habilita [Authorize]
// Registrar MediatR y escanear los Handlers en la capa de Aplicación
builder.Services.AddMediatR(cfg =>
{
    // Escanea el ensamblado donde definimos el comando de creación de producto
    cfg.RegisterServicesFromAssembly(typeof(ECommerce.Application.Interfaces.IProductRepository).Assembly);

    // REGRA CRÍTICA: Registrar el ValidationBehavior en el pipeline de MediatR
    cfg.AddOpenBehavior(typeof(ECommerce.Application.Behaviors.ValidationBehavior<,>));
});
// Registrar el Manejador Global de Excepciones (.NET 8) y el soporte para ProblemDetails (RFC 7807)
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// 2. CONFIGURACIÓN DEL PIPELINE DE MIDDLEWARES
// El middleware de excepciones DEBE SER EL PRIMERO
// de esta forma puede envolver y capturar los errores de cualquier componente que falle abajo.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Habilita la autenticación
app.UseAuthorization();

// Mapear las rutas hacia los Controladores
app.MapControllers();

app.Run();
