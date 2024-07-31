using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieTicketingApplication.Data;
using MovieTicketingApplication.Models;

namespace MovieTicketingApplication.Controllers
{
    [Route("api/Theatre")]
    [ApiController]
    public class TheatresController : ControllerBase
    {
        private readonly TheatreContext _theatreContext;
        private readonly BookingContext _bookingContext;

        public TheatresController(TheatreContext context, BookingContext bookingContext)
        {
            _theatreContext = context;
            _bookingContext = bookingContext;
        }

        // GET: api/Theatres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theatre>>> GetTheatres()
        {
            var theatres = await _theatreContext.Theatres.ToListAsync();
            return Ok(theatres);
        }

        // GET: api/Theatres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Theatre>> GetTheatre(int id)
        {
            var theatre = await _theatreContext.Theatres.FindAsync(id);

            if (theatre == null)
            {
                return NotFound();
            }

            return Ok(theatre) ;
        }

        // PUT: api/Theatres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTheatre(int id, Theatre theatre)
        {
            if (id != theatre.Id)
            {
                return BadRequest();
            }

            _theatreContext.Entry(theatre).State = EntityState.Modified;

            try
            {
                await _theatreContext.SaveChangesAsync();
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Theatre>> PostTheatre(Theatre theatre)
        {
            _theatreContext.Theatres.Add(theatre);
            await _theatreContext.SaveChangesAsync();

            return CreatedAtAction("GetTheatre", new { id = theatre.Id }, theatre);
        }

        // DELETE: api/Theatres/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTheatre(int id)
        {
            var theatre = await _theatreContext.Theatres.FindAsync(id);
            if (theatre == null)
            {
                return NotFound();
            }

            _theatreContext.Theatres.Remove(theatre);
            await _theatreContext.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Theatres/5/Bookings
        [HttpGet("{id}/Bookings")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsOfTheatres(int id)
        {
            var bookings = await _bookingContext.Bookings.Where(b => b.Theatreid == id).ToListAsync();
            return Ok(bookings);
        }


        private bool TheatreExists(int id)
        {
            return _theatreContext.Theatres.Any(e => e.Id == id);
        }
    }
}
