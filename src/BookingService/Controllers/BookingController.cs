using BookingService.Data;
using BookingService.Models;
using Microsoft.AspNetCore.Mvc;
using BookingService.Services;
using BookingService.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Controllers{

    [ApiController]
    [Route("api/[controller]")]
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
        [ProducesResponseType(typeof(BookingResponse), 200)]
        public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] Booking booking)
        {
            var space = await _spacesClient.GetSpaceAsync(booking.SpaceId);

            if (space == null)
                return BadRequest("El espacio no existe o no está disponible");

            var existing = await _context.Bookings
                .Where(b => b.SpaceId == booking.SpaceId)
                .ToListAsync();

            var hasOverlap = existing.Any(b =>
                booking.Start < b.End &&
                booking.End > b.Start
            );

            if (hasOverlap)
                return Conflict("Ya existe una reserva que se solapa con este horario.");

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new BookingResponse
            {
                Message = "Reserva creada",
                Name = space.Name
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Booking>), 200)]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            var booking = await _context.Bookings
                .AsNoTracking()
                .ToListAsync();
            return Ok(booking);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Booking),200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null) return NotFound();

            return Ok(booking);
        }
    }
}
