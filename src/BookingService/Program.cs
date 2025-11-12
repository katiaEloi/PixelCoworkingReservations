using BookingService.Data;
using BookingService.Services;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Conexión a la base de datos (usa la variable del docker-compose)
var conn = builder.Configuration.GetConnectionString("Default")
           ?? builder.Configuration["ConnectionStrings:Default"]
           ?? "Server=sqlserver;Database=BookingsDb;User Id=sa;Password=Your_strong_password123!;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conn));

// 🔹 Inyección de dependencias
builder.Services.AddHttpClient<SpacesClient>(client =>
{
    // La URL del SpacesService (ajústala si usas otro puerto)   
    client.BaseAddress = new Uri("http://pixel-spaces:8081");
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Middleware de Swagger
app.UseSwagger();
app.UseSwaggerUI();

// 🔹 Rutas de controladores
app.MapControllers();

// 🔹 Crear la base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
