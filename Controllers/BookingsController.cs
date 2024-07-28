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
    public class BookingsController : ControllerBase
    {
        private readonly BookingContext _bookingContext;
        private readonly UserContext _userContext;
        private readonly MovieContext _movieContext;
        private readonly TheatreContext _theatreContext;

        public BookingsController(BookingContext bookingContext, UserContext userContext, MovieContext movieContext, TheatreContext theatreContext)
        {
            _bookingContext = bookingContext;
            _userContext = userContext;
            _theatreContext = theatreContext;
            _movieContext = movieContext;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _bookingContext.Bookings.ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(long id)
        {
            var booking = await _bookingContext.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            // Get details of user
            var user = await _userContext.Users.FindAsync(booking.Userid);
            if (user == null)
            {
                return NotFound("User with Id {booking.Userid} is not found");
            }

            // Get details of movie
            var movie = await _movieContext.Movies.FindAsync(booking.Movieid);
            if (movie == null)
            {
                return NotFound("Movie with Id {booking.Movieid} is not found");
            }

            // Get details of theatre
            var theatre = await _theatreContext.Theatres.FindAsync(booking.Theatreid);
            if (theatre == null)
            {
                return NotFound("Theatre with Id {booking.Theatreid} is not found");
            }

            booking.User = user;
            booking.Movie = movie;
            booking.Theatre = theatre;

            _bookingContext.Bookings.Add(booking);
            await _bookingContext.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(long id)
        {
            var booking = await _bookingContext.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _bookingContext.Bookings.Remove(booking);
            await _bookingContext.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(long id)
        {
            return _bookingContext.Bookings.Any(e => e.Id == id);
        }
    }
}
