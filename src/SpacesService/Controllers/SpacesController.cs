using Microsoft.AspNetCore.Mvc;
using SpacesService.Dtos;
using SpacesService.Models;
using SpacesService.Data;


namespace SpacesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpacesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SpacesController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/spaces
        [HttpGet]
        public ActionResult<IEnumerable<SpaceDto>> GetSpaces()
        {
            var spaces = _context.Spaces
            .Select(s => new SpaceDto
            {
                Id = s.Id,
                Name = s.Name,
                Capacity = s.Capacity,
                IsPrivate = s.IsPrivate
            }).ToList();

            return Ok(spaces);
        }

        //GET: api/spaces/5
       [HttpGet("{id}")]
        public ActionResult<SpaceDto> GetSpaces(int id)
        {
            var space = _context.Spaces.Where(s => s.Id == id)
                .Select(s => new SpaceDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Capacity = s.Capacity,
                    IsPrivate = s.IsPrivate
                })
                .FirstOrDefault();

            if (space == null) 
                return NotFound();

            return Ok(space);
        }

        // POST: api/spaces
        [HttpPost]
        public ActionResult<Space> CreateSpace(Space space)
        {
            _context.Spaces.Add(space);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSpaces), new { id = space.Id} , space);
        }

        // PUT: api/spaces/5
        [HttpPut("{id}")]
        public ActionResult<Space> GetSpaces(int id, Space updated)
        {
            var space = _context.Spaces.Find(id);
            if (space == null)
                return NotFound();
            
            space.Name = updated.Name;
            space.Capacity = updated.Capacity;
            space.IsPrivate = updated.IsPrivate;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/spaces/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSpace(int id)
        {
            var space = _context.Spaces.Find(id);
            if (space == null)
                 return NotFound();

            _context.Spaces.Remove(space);
            _context.SaveChanges();
            return NoContent();
        }


            










    }
}
