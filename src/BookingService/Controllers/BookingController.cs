using BookingService.Data;
using BookingService.Models;
using Microsoft.AspNetCore.Mvc;
using BookingService.Services;
using BookingService.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BookingService.Controllers{

    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SpacesClient _spacesClient;

        public BookingController(AppDbContext context, SpacesClient spacesClient)
        {
            _context = context;
            _spacesClient = spacesClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody]Booking booking)
        {
            var space = await _spacesClient.GetSpaceAsync(booking.SpaceId);

            if (space == null)
                return BadRequest("El espacio no existe o no está disponible");

            // Aquí podrías añadir lógica de validación, disponibilidad, etc.
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();                  

            return Ok( new
            {
                Message = $"Reserva creada para el espacio:{space.Name}" ,
                Capacity = space.Capacity,
                IsPrivate = space.IsPrivate

            });
        }
    }
}
