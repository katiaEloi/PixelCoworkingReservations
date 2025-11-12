# Pixel Coworking – Reservations API (.NET 9, Microservicios)

Dos microservicios Minimal API:
- **SpacesService**: gestión de espacios (coworking rooms/desks).
- **BookingService**: gestión de reservas.

Incluye: Dockerfile, docker-compose, Swagger, EF Core (SqlServer), esqueleto CQRS con MediatR (pendiente), y colección Postman.

## Requisitos
- .NET 9 SDK
- Docker Desktop
- SQL Server (usaremos contenedor)
- Postman (opcional)

## Estructura
```
/src
  /SpacesService
  /BookingService
/infra
  docker-compose.yml
/.github/workflows
  ci.yml
/postman
  pixel-coworking.postman_collection.json
```

## Puertos
- SpacesService: http://localhost:8081
- BookingService: http://localhost:8082
- SQL Server: localhost:1433 (en contenedor)

## Primeros pasos (local)
```bash
# Restaurar paquetes
dotnet restore

# (Opcional) Añadir paquetes EF Core y Swagger si faltan:
dotnet add src/SpacesService/SpacesService.csproj package Microsoft.EntityFrameworkCore
dotnet add src/SpacesService/SpacesService.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add src/SpacesService/SpacesService.csproj package Microsoft.EntityFrameworkCore.Tools
dotnet add src/SpacesService/SpacesService.csproj package Swashbuckle.AspNetCore
dotnet add src/BookingService/BookingService.csproj package Microsoft.EntityFrameworkCore
dotnet add src/BookingService/BookingService.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add src/BookingService/BookingService.csproj package Microsoft.EntityFrameworkCore.Tools
dotnet add src/BookingService/BookingService.csproj package Swashbuckle.AspNetCore

# Ejecutar servicios con Docker
docker compose -f infra/docker-compose.yml up --build
```

## Migraciones EF Core (ejemplo)
```
# Desde cada proyecto (si usas Migrations)
dotnet ef migrations add InitialCreate -p src/SpacesService -s src/SpacesService
dotnet ef database update -p src/SpacesService -s src/SpacesService

dotnet ef migrations add InitialCreate -p src/BookingService -s src/BookingService
dotnet ef database update -p src/BookingService -s src/BookingService
```

> Nota: Para simplicidad, el código usa `EnsureCreated()` en arranque. Recomendado migraciones en entornos reales.

## CI (GitHub Actions)
- Build + restore con .NET 9
- (Puedes ampliar a tests y despliegue).

## Azure (sugerido)
- App Service para cada microservicio (contenedor).
- Azure SQL o SQL en contenedor administrado.
- Azure Blob Storage (si necesitas ficheros).
- Azure Functions para tareas asíncronas (notificaciones).
