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
    public class MoviesController : ControllerBase
    {
        private readonly movieBookingDBContext _context;

        public MoviesController(movieBookingDBContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Movies/5/Reviews
        [HttpGet("{id}/Reviews")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsOfMovie(int id)
        {
            var reviews = await _context.Reviews.Where(b => b.MovieId == id).ToListAsync();
            if (reviews == null)
            {
                return NoContent();
            }
            return Ok(reviews);
        }

        // GET: api/Movies/5/Screens
        [HttpGet("{id}/Screens")]
        public async Task<ActionResult<IEnumerable<Screen>>> GetScreensOfMovie(int id)
        {
            var screens = await _context.Screens.Where(s => s.MovieId == id).ToListAsync();
            if (screens == null)
            {
                return NoContent();
            }
            return Ok(screens);
        }

        // GET: api/Movies/filter?location=location&language=language&genre=genre
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetFilteredMovies(string? location, string? language, string? genre)
        {
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(m => _context.Screens
                    .Any(s => s.MovieId == m.Id && _context.Theatres.Any(t => t.Id == s.TheatreId && t.Location == location)));
            }

            if (!string.IsNullOrEmpty(language))
            {
                query = query.Where(m => m.Lang.Equals(language, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(m => m.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));
            }

            var movies = await query.ToListAsync();

            if (!movies.Any())
            {
                return NotFound("No movies found for the specified filters.");
            }

            return Ok(movies);
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
