
## Pixel Coworking – Microservicios de Reservas (Spaces + Bookings)
## 🧩 Descripción general  
Proyecto de ejemplo desarrollado en **.NET 8** con **microservicios**, contenedores **Docker** y base de datos **SQL Server 2022**.  
Simula un sistema de reservas para *Pixel-Coworking Alicante*, con gestión de espacios y validaciones de disponibilidad.

🟦 .NET 8 / ASP.NET Core
🐳 Docker & Docker Compose
🗄️ SQL Server en contenedor
🧩 Microservicios
🔄 Comunicación HTTP entre servicios
🧪 Validación de solapamiento de reservas
📘 Swagger
🧱 Arquitectura del Proyecto

## ⚙️ Arquitectura del proyecto

infra/
├── docker-compose.yml
src/
├── SpacesService/
│ ├── Controllers/
│ ├── Data/
│ ├── Models/
│ └── Program.cs
├── BookingService/
├── Controllers/
├── Data/
├── Dtos/
├── Services/
└── Program.cs

## 🏗️ Diagrama de arquitectura (Docker + Microservicios)

                 ┌────────────────────────────┐
                 │        Docker Host         │
                 │    (pixel-network bridge)  │
                 └────────────────────────────┘
                           │
           ┌──────────────────────────────────────────┐
           │                                          │
           ▼                                          ▼
┌──────────────────────┐                 ┌─────────────────────────┐
│   SpacesService      │  <──────▶─────  │     BookingService      │
│  (http://:8081)      │  Validate       │   (http://:8082)       │
│  Administra espacios │  disponibilidad │  Crea y gestiona        │
└──────────────────────┘                 │  reservas               │
           │                             └─────────────────────────┘
           │                                       │
           ▼                                       ▼
        ┌────────────────────────────────────────────────┐
        │                SQL Server 2022                  │
        │ Databases:  SpacesDb  &  BookingsDb             │
        └────────────────────────────────────────────────┘

🏁 Cómo ejecutar el proyecto con Docker

Asegúrate de estar dentro de la carpeta:
infra/
Luego ejecuta:
docker compose up -d --build

Esto creará:

Servicio	Puerto
SpacesService	8081
BookingService	8082
SQL Server	1433
🌐 Accesos Rápidos a Swagger
🟦 SpacesService

👉 http://localhost:8081/swagger/index.html

Endpoints:

GET /api/Spaces
POST /api/Spaces
PUT /api/Spaces/{id}
DELETE /api/Spaces/{id}
🟩 BookingService

👉 http://localhost:8082/swagger/index.html

Endpoints:

GET /api/Booking
GET /api/Booking/{id}
POST /api/Booking (con validación de solapamiento)

🧪 Ejemplo de petición POST (Booking)
{
  "spaceId": 1,
  "userName": "katia",
  "start": "2025-11-11T10:00:00",
  "end": "2025-11-11T11:00:00"
}

✔️ Si la sala existe
✔️ Si no hay solapamiento

Respuesta:
{
  "message": "Reserva creada",
  "name": "Sala Reuniones Pixel"
}


❌ Si existe solapamiento:

"Ya existe una reserva que se solapa con este horario."

🐳 Docker tips
Ver contenedores activos
docker ps

Ver logs de un servicio
docker logs pixel-bookings

Parar y eliminar contenedores
docker compose down

Parar + borrar BD
docker compose down -v


⚠️ Solo usar -v cuando realmente quieras reiniciar las bases de datos.

🔐 Variables de entorno (.env)

El archivo .env está excluido con .gitignore para no subir claves sensibles.

📘 Comandos Git — Glosario Completo

Un resumen claro para recordar siempre:

🔵 git status

Muestra el estado de tu repositorio:
qué archivos cambiaron
qué está listo para commit
si tu rama está ahead/behind del remoto

🟢 git add

Añade cambios al área de preparación (staging):
git add .
git add README.md

🟣 git commit

Guarda los cambios en tu rama local:
git commit -m "Mensaje del commit"

🟠 git push

Envía tus commits al repositorio remoto:

git push

🟡 git pull

Descarga y fusiona cambios remotos:

git pull

🟤 git fetch

Descarga solo la información del remoto, sin mezclar:

git fetch


Sirve para ver qué hay nuevo antes de mezclarlo.

⚫ git pull --rebase

Actualiza tu trabajo encima de los cambios remotos (mucho más limpio):

git pull --rebase

🔴 Commit All (Visual Studio)

Hace automáticamente:

git add .

git commit -m "mensaje"

En un solo click.

🔵 Pull then Push (Visual Studio)

Hace:

git pull

si va bien → git push

👩‍💻 Autora

Katia Barrón
Ingeniera de Informática – Desarrollo .NET & Microservicios
Proyecto: Pixel Coworking – Sistema de reservas con Docker y microservicios

🌐 Pixel-Coworking (Alicante)
💻 Stack principal: .NET, C#, ASP.NET Core, SQL Server, Docker
✉️ www.linkedin.com/in/katiaeloianalista10
