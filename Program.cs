// using EventBookingAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database ulash
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();  // ✅ Controller-larni qo‘shish
// Swagger qo‘shish
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")  // Angular domenini qo'shish
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Permitir solicitudes desde Angular
                  .AllowAnyMethod() // Permitir GET, POST, PUT, DELETE, etc.
                  .AllowAnyHeader() // Permitir todos los headers
                  .AllowCredentials(); // Si estás usando autenticación
        });
});

var app = builder.Build();

// Usar CORS antes de mapear los controladores
app.UseCors("AllowAngularApp");

app.MapControllers();

app.Run();

