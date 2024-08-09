using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketingApplication.Models;

namespace MovieTicketingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheatresController : ControllerBase
    {
        private readonly movieBookingDBContext _context;

        public TheatresController(movieBookingDBContext context)
        {
            _context = context;
        }

        // GET: api/Theatres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theatre>>> GetTheatres()
        {
            return await _context.Theatres.ToListAsync();
        }

        // GET: api/Theatres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Theatre>> GetTheatre(int id)
        {
            var theatre = await _context.Theatres.FindAsync(id);

            if (theatre == null)
            {
                return NotFound();
            }

            return theatre;
        }

        // PUT: api/Theatres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTheatre(int id, Theatre theatre)
        {
            if (id != theatre.Id)
            {
                return BadRequest();
            }

            _context.Entry(theatre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TheatreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Theatres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Theatre>> PostTheatre(Theatre theatre)
        {
            _context.Theatres.Add(theatre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTheatre", new { id = theatre.Id }, theatre);
        }

        // DELETE: api/Theatres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheatre(int id)
        {
            var theatre = await _context.Theatres.FindAsync(id);
            if (theatre == null)
            {
                return NotFound();
            }

            _context.Theatres.Remove(theatre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Theatres/5/Screens
        [HttpGet("{id}/Screens")]
        public async Task<ActionResult<IEnumerable<Screen>>> GetScreensOfTheatre(int id)
        {
            var screens = await _context.Screens.Where(s => s.TheatreId == id).ToListAsync();
            if (screens == null)
            {
                return NoContent();
            }
            return Ok(screens);
        }

        // GET: api/Theatres/filter?location=location
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Theatre>>> GetTheatresOnLocation(string? location)
        {
            var theatres = await _context.Theatres.Where(t => t.Location == location).ToListAsync();
            if (theatres == null)
            {
                return NoContent();
            }
            return Ok(theatres);
        } 


        private bool TheatreExists(int id)
        {
            return _context.Theatres.Any(e => e.Id == id);
        }
    }
}
