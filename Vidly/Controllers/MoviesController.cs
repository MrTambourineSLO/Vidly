using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
      
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var genres = _context.Genres;
            var movieToEdit = _context.Movies.Single(m => m.Id == id);
            var viewModel = new NewMovieViewModel(movieToEdit)
            {
                Genres = genres
            };
            if (movieToEdit == null)
            {
                return HttpNotFound();
            }

            return View("MovieForm",viewModel);
        }
        //[HttpPost]
        //public ActionResult Edit(Movie movie)
        //{
            
        //    if (movie == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        var movieToEdit = _context.Movies.Single(t => t.Id == movie.Id);
        //        movieToEdit.GenreId = movie.GenreId;
        //        movieToEdit.Name = movie.Name;
        //        movieToEdit.NumberInStock = movie.NumberInStock;
        //        movieToEdit.ReleaseDate = movie.ReleaseDate;
        //        movieToEdit.Id = movie.Id;
                
        //    }
        //    _context.SaveChanges();

        //    return RedirectToAction("Index","Movies");
        //}

        public ActionResult Index()
        {
            if(User.IsInRole(RoleName.CanManageMovies))
            return View("List");
            return View("ReadOnlyList");
        }
        [Route("movies/details/{id}")]
        public ActionResult Details(byte id)
        {
            var movie = _context.Movies.Include(g => g.Genre).SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            return View(movie);

        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new NewMovieViewModel()
            {
                
                Genres = genres
                
            };
            return View("MovieForm",viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Save(Movie movie)
        {
            //movie.Added = DateTime.Today;
            if (!ModelState.IsValid)
            {
                var viewModel = new NewMovieViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm",viewModel);
            }
            if (movie.Id == 0)
            {
                //Add date time in backend to prevent SQL Injection
                movie.Added = DateTime.Now;
                _context.Movies.Add(movie); 
            }
            else
            {
                var movieToEdit = _context.Movies.Single(t => t.Id == movie.Id);
                movieToEdit.GenreId = movie.GenreId;
                movieToEdit.Name = movie.Name;
                movieToEdit.NumberInStock = movie.NumberInStock;
                movieToEdit.ReleaseDate = movie.ReleaseDate;
                movieToEdit.Id = movie.Id;
                   
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Movies");


        }
    }
}