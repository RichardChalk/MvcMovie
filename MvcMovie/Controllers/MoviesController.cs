#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.ViewModels;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        // GET: Movies
        // GET: Movies
        // GET: Movies
        public async Task<IActionResult> Index(string q)
        {
            //The query is only defined at this point, it has not been run against the database
            var movies = _context.Movie
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    Genre = m.Genre,
                    Price = m.Price,
                });
                

            // Check search string
            if (!String.IsNullOrEmpty(q))
            {
                movies = movies.Where(s => s.Title.Contains(q));
            }

            return View(movies);
        }

        //[HttpPost]
        //public string Index(string q, bool notUsed)
        //{
        //    return "From [HttpPost]Index: filter on " + q;
        //}

        // GET: Movies/Details/5
        // GET: Movies/Details/5
        // GET: Movies/Details/5
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDb = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);

            var movieViewModel = new MovieViewModel()
            {
                Id = movieDb.Id,
                Title = movieDb.Title,
                ReleaseDate= movieDb.ReleaseDate,
                Genre= movieDb.Genre,
                Price= movieDb.Price,
            };
                
            if (movieDb == null)
            {
                return NotFound();
            }

            return View(movieViewModel);
        }

        // GET: Movies/Create
        // GET: Movies/Create
        // GET: Movies/Create
        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // POST: Movies/Create
        // POST: Movies/Create
        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModels.MovieViewModel movie)
        {
            if (ModelState.IsValid)
            {
                var movieDb = _context.Movie.FirstOrDefault(m=>m.Id == movie.Id);
                movieDb.Id = movie.Id;
                movieDb.Title = movie.Title;
                movieDb.ReleaseDate = movie.ReleaseDate;
                movieDb.Genre = movie.Genre;
                movieDb.Price = movie.Price;    
                
                _context.Add(movieDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        // GET: Movies/Edit/5
        // GET: Movies/Edit/5
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDb = await _context.Movie.FindAsync(id);
            
            if (movieDb == null)
            {
                return NotFound();
            }

            var movieViewModel = new MovieViewModel();
            movieViewModel.Id = movieDb.Id;
            movieViewModel.Title = movieDb.Title;   
            movieViewModel.Genre = movieDb.Genre;
            movieViewModel.ReleaseDate = movieDb.ReleaseDate;
            movieViewModel.Price = movieDb.Price;
            
            return View(movieViewModel);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        public async Task<IActionResult> Edit(ViewModels.MovieViewModel movie)
        {
            if (movie.Id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var movieDb = new Movie();
                try
                {
                    movieDb.Id = movie.Id;
                    movieDb.Title = movie.Title;
                    movieDb.Genre = movie.Genre;
                    movieDb.ReleaseDate = movie.ReleaseDate;
                    movieDb.Price = movie.Price;

                    _context.Update(movieDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntityExists(movieDb.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        // GET: Movies/Delete/5
        // GET: Movies/Delete/5
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDb = await _context.Movie.FindAsync(id);

            if (movieDb == null)
            {
                return NotFound();
            }

            var movieViewModel = new MovieViewModel();
            movieViewModel.Id = movieDb.Id;
            movieViewModel.Title = movieDb.Title;
            movieViewModel.Genre = movieDb.Genre;
            movieViewModel.ReleaseDate = movieDb.ReleaseDate;
            movieViewModel.Price = movieDb.Price;

            return View(movieViewModel);
        }

        // POST: Movies/Delete/5
        // POST: Movies/Delete/5
        // POST: Movies/Delete/5
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieDb = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movieDb);
            await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
            return RedirectToAction("Index");
        }

        private bool EntityExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
