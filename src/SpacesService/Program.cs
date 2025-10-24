using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("Default") 
           ?? builder.Configuration["ConnectionStrings:Default"] 
           ?? "Server=localhost;Database=SpacesDb;User Id=sa;Password=Your_strong_password123!;TrustServerCertificate=True;";

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

app.MapGet("/", () => Results.Ok(new { service = "SpacesService", status = "ok" }));

app.MapGet("/api/spaces", async (AppDbContext db) =>
    await db.Spaces.AsNoTracking().ToListAsync());

app.MapGet("/api/spaces/{id:int}", async (int id, AppDbContext db) =>
    await db.Spaces.FindAsync(id) is { } s ? Results.Ok(s) : Results.NotFound());

app.MapPost("/api/spaces", async (Space s, AppDbContext db) =>
{
    db.Spaces.Add(s);
    await db.SaveChangesAsync();
    return Results.Created($"/api/spaces/{s.Id}", s);
});

app.MapPut("/api/spaces/{id:int}", async (int id, Space input, AppDbContext db) =>
{
    var s = await db.Spaces.FindAsync(id);
    if (s is null) return Results.NotFound();
    s.Name = input.Name;
    s.Capacity = input.Capacity;
    s.IsPrivate = input.IsPrivate;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/spaces/{id:int}", async (int id, AppDbContext db) =>
{
    var s = await db.Spaces.FindAsync(id);
    if (s is null) return Results.NotFound();
    db.Spaces.Remove(s);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Space> Spaces => Set<Space>();
}

class Space
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public bool IsPrivate { get; set; }
}
