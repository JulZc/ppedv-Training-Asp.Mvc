using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Videothek.ViewModels;
using System.Data.Entity;
using VideothekData;
using VideothekData.Models;
using System.Threading.Tasks;

namespace Videothek.Controllers
{
    [Authorize(Roles= "CanManageMovie")]
    public class MovieController : Controller
    {
        /* GET: Movie
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            //http://localhost:62375/?page=2&sortBy=Genre
            //return RedirectToAction("Index", "Home", new { page = 2, sortBy="Genre" });
            //return HttpNotFound();
            //return View();

            return Content($"Page: {pageIndex} sorted by {sortBy}"); //string ohne Html
        }

        [Route("movie/released/{year:range(1920, 2020)}/{month:range(1,12)}", Name = "ByReleaseDate")]
        public ActionResult ByReleaseDate(int year, byte month)
        {
            return Content($"{month}/{year}");
        }        
        public ActionResult Random()
        {//returns ViewModel
            RandomMovieViewModel model = new RandomMovieViewModel
            {
                Movie = new Movie { Name= "Kill Bill"},
                Customers = new List<Customer>
                {
                    new Customer { Name= "Max Mayer" },
                    new Customer { Name= "John Dow" }
                }
            };
            return View(model);
        }*/

        private readonly VideothekContext _context;
        public MovieController()
        {
            _context = new VideothekContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            List<Movie> movies = await _context.Movies.Include(m => m.Genres).ToListAsync();

            return View(movies);
        }
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            Movie movie = await _context.Movies.Include(m => m.Genres).SingleAsync(m => m.Id == id);

            return View(movie);
        }


        public async Task<ActionResult> New()
        {
            MovieFormViewModel model = new MovieFormViewModel
            {
                Genres = await _context.Genres.
                                    Select(g => new GenreVm
                                    {
                                        Id = g.Id,
                                        Name = g.Name
                                    }).ToListAsync()
            };

            return View("Form", model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            MovieFormViewModel model = new MovieFormViewModel
            {
                Movie =  await _context.Movies.Include(m => m.Genres).SingleAsync(m => m.Id == id),
                Genres = await _context.Genres.Select(g => new GenreVm { Id = g.Id, Name = g.Name }).ToListAsync()
            };

            var genreIds = model.Movie.Genres.Select(g => g.Id);
            Parallel.ForEach(model.Genres.Where(g => genreIds.Contains(g.Id)),
                             genre => genre.IsChecked = true);

            return View("Form", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(MovieFormViewModel model)
        {
            IEnumerable<int> genreIds = model.Genres.Where(g => g.IsChecked).Select(g => g.Id);

            if (!genreIds.Any())
                ModelState.AddModelError("MinOneGenre", "Movie should have at least one Genre");

            if (!ModelState.IsValid)
            {
                //model.Genres = _context.Genres.Select(g => new GenreVm { Id = g.Id, Name = g.Name }).ToList();
                //Parallel.ForEach(model.Genres.Where(g => genreIds.Contains(g.Id)), g => g.IsChecked = true);

                model.Genres.ForEach(async g => g.Name = (await _context.Genres.FindAsync(g.Id)).Name);
                return View("Form", model);
            }

            List<Genre> genresOnDb = await _context.Genres.Where(g => genreIds.Contains(g.Id)).ToListAsync();
            model.Movie.Genres = new List<Genre>();


            if (model.Movie.Id == null)
            {
                model.Movie.Genres.AddRange(genresOnDb);
                _context.Movies.Add(model.Movie);
            }
            else
            {
                _context.Entry(model.Movie).State = EntityState.Modified;

                Movie movieOnDb = await _context.Movies.Include(m => m.Genres).SingleAsync(m => m.Id == model.Movie.Id);
                movieOnDb.Genres.RemoveAll(g => true);
                movieOnDb.Genres.AddRange(genresOnDb);
            }


            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}