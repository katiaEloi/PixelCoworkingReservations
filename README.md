# 🇪🇸 Pixel-Coworking Reservations Starter

## 🧩 Descripción general  
Proyecto de ejemplo desarrollado en **.NET 8** con **microservicios**, contenedores **Docker** y base de datos **SQL Server 2022**.  
Simula un sistema de reservas para *Pixel-Coworking Alicante*, con gestión de espacios y validaciones de disponibilidad.

---

## ⚙️ Arquitectura del proyecto
```
infra/
 ├── docker-compose.yml
src/
 ├── SpacesService/
 │    ├── Controllers/
 │    ├── Data/
 │    ├── Models/
 │    └── Program.cs
 ├── BookingService/
      ├── Controllers/
      ├── Data/
      ├── Dtos/
      ├── Services/
      └── Program.cs
```

---

## 🏗️ Diagrama de arquitectura (Docker + Microservicios)
```
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
```

---

## 🐳 Ejecución con Docker

### 1️⃣ Levantar contenedores
```bash
cd infra
docker compose up -d --build
```

### 2️⃣ Acceder a los servicios
- **SpacesService** → http://localhost:8081/swagger  
- **BookingService** → http://localhost:8082/swagger  

### 3️⃣ Verificar contenedores activos
```bash
docker ps
```

### 4️⃣ Detener los servicios
```bash
docker compose down
```

> ⚠️ Usa `docker compose down -v` **solo** si quieres borrar las bases de datos (volúmenes incluidos).

---

## 🧠 Endpoints principales

### SpacesService (`http://localhost:8081/api/spaces`)
| Método | Endpoint | Descripción |
|:--:|:--|:--|
| GET | `/api/spaces` | Obtiene todos los espacios |
| GET | `/api/spaces/{id}` | Obtiene un espacio por ID |
| POST | `/api/spaces` | Crea un nuevo espacio |
| PUT | `/api/spaces/{id}` | Actualiza un espacio |
| DELETE | `/api/spaces/{id}` | Elimina un espacio |

### BookingService (`http://localhost:8082/api/booking`)
| Método | Endpoint | Descripción |
|:--:|:--|:--|
| POST | `/api/booking` | Crea una nueva reserva (valida con SpacesService) |
| GET | `/api/booking` | Lista todas las reservas |
| GET | `/api/booking/{id}` | Consulta una reserva por ID |

---

## 🧾 Ejemplo de reserva válida
```json
{
  "spaceId": 1,
  "userName": "katia",
  "start": "2025-11-11T10:00:00",
  "end": "2025-11-11T11:00:00"
}
```

📩 **Respuesta:**
```json
{
  "message": "Reserva creada",
  "name": "Sala Reuniones Pixel"
}
```

---

## 🔒 Configuración de variables (.env)
```
SA_PASSWORD=Your_strong_password123!
ASPNETCORE_ENVIRONMENT=Production
```

> 📁 El archivo `.env` está **excluido del repositorio** mediante `.gitignore` para proteger credenciales.

---

## 🧰 Tecnologías utilizadas
- .NET 8 (C#)
- Entity Framework Core
- ASP.NET Web API
- SQL Server 2022 (Docker)
- Docker Compose
- Swagger UI
- RESTful JSON APIs

---

## ✨ Autor
👩‍💻 **Katia Barrón**  
Ingeniera informática y fundadora de [**Pixel-Coworking Alicante**](https://pixel-coworking.com/)  
Desarrollo, infraestructura y diseño de microservicios para coworking y espacios flexibles.

---

# 🇬🇧 Pixel-Coworking Reservations Starter

## 🧩 Overview
A sample project built with **.NET 8**, **Docker**, and **SQL Server 2022**, demonstrating a clean **microservices architecture** for a coworking reservation system.

---

## 🏗️ Architecture Diagram
```
[SpacesService] ⇄ [BookingService] ⇄ [SQL Server]
      :8081             :8082             :1433
```

---

## 🐳 Run with Docker
```bash
cd infra
docker compose up -d --build
```

Then visit:
- http://localhost:8081/swagger → Spaces Service  
- http://localhost:8082/swagger → Booking Service  

Stop all containers:
```bash
docker compose down
```

---

## 💡 Author
👩‍💻 **Katia Barrón** — Software Engineer & Founder at *Pixel-Coworking Alicante*  
Building modular, cloud-ready applications for flexible workspaces and digital entrepreneurs.
