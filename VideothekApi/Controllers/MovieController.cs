using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using VideothekApi.DataTransferObjects;
using VideothekData;
using VideothekData.Models;

namespace VideothekApi.Controllers
{
    public class MovieController : ApiController
    {
        private readonly VideothekContext _context;
        public MovieController()
        {
            _context = new VideothekContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Select |
                                           AllowedQueryOptions.OrderBy,
                                           MaxOrderByNodeCount = 100, MaxNodeCount = 1000)]
        public async Task<IHttpActionResult> Get()
        {
            List<Movie> movies = await _context.Movies.ToListAsync();

            return Ok(movies).Cached(cacheability: Cacheability.Public, maxAge: TimeSpan.FromMinutes(1));
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            Movie movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [Route("api/genre/{id:int}/movie")]
        public async Task<IHttpActionResult> GetByGenreId(int id)
        {
            var movies = await _context.Movies.Include(m => m.Genres)
                                              .Where(m => m.Genres.Any(g => g.Id == id))
                                              .ToListAsync();
            movies.ForEach(m => m.Genres.ForEach(g => g.Movies = null));

            return Ok(movies);
        }
        [Route("api/genre/{name:alpha}/movie")]
        public async Task<IHttpActionResult> GetByGenreName(string name)
        {
            var movies = await _context.Movies.Include(m => m.Genres)
                                              .Where(m => m.Genres.Any(g => g.Name == name))
                                              .ToListAsync();

            movies.ForEach(m => m.Genres.ForEach(g => g.Movies = null));
            
            return Ok(movies);
        }


        public async Task<IHttpActionResult> Post([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Create
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(routeName: "DefaultApi", routeValues: new { id = movie.Id }, content: movie);
        }

        public async Task<IHttpActionResult> Put([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Update
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            //Delete
            Movie movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
