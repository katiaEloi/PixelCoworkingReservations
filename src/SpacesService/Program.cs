using Microsoft.EntityFrameworkCore;
using SpacesService.Data;
using SpacesService.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Cadena de conexión (usa variable de entorno o valor por defecto)
var conn = builder.Configuration.GetConnectionString("Default")
           ?? builder.Configuration["ConnectionStrings:Default"]
           ?? "Server=localhost;Database=SpacesDb;User Id=sa;Password=Your_strong_password123!;TrustServerCertificate=True;";

// ✅ Registrar DbContext con SQL Server
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conn));

// ✅ Agregar servicios básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Configurar Swagger
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Usar controladores (como SpacesController)
app.MapControllers();

// ✅ Crear base de datos si no existe
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// ✅ Endpoint raíz informativo
app.MapGet("/", () => Results.Ok(new { service = "SpacesService", status = "ok" }));

app.Run();