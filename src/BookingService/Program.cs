using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("Default") 
           ?? builder.Configuration["ConnectionStrings:Default"] 
           ?? "Server=localhost;Database=BookingsDb;User Id=sa;Password=Your_strong_password123!;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conn));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/", () => Results.Ok(new { service = "BookingService", status = "ok" }));

app.MapGet("/api/bookings", async (AppDbContext db) =>
    await db.Bookings.AsNoTracking().ToListAsync());

app.MapGet("/api/bookings/{id:int}", async (int id, AppDbContext db) =>
    await db.Bookings.FindAsync(id) is { } b ? Results.Ok(b) : Results.NotFound());

app.MapPost("/api/bookings", async (Booking b, AppDbContext db) =>
{
    // Validación simple (ejemplo): fecha fin > inicio
    if (b.End <= b.Start) return Results.BadRequest(new { message = "End must be after Start" });
    db.Bookings.Add(b);
    await db.SaveChangesAsync();
    return Results.Created($"/api/bookings/{b.Id}", b);
});

app.MapPut("/api/bookings/{id:int}", async (int id, Booking input, AppDbContext db) =>
{
    var b = await db.Bookings.FindAsync(id);
    if (b is null) return Results.NotFound();
    b.SpaceId = input.SpaceId;
    b.UserName = input.UserName;
    b.Start = input.Start;
    b.End = input.End;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/bookings/{id:int}", async (int id, AppDbContext db) =>
{
    var b = await db.Bookings.FindAsync(id);
    if (b is null) return Results.NotFound();
    db.Bookings.Remove(b);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Booking> Bookings => Set<Booking>();
}

class Booking
{
    public int Id { get; set; }
    public int SpaceId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
