using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        //public ActionResult Random()
        //{
        //    var movie = new Movie(){Name = "Shrek"};
        //    //Create a list of cutomers
        //    var customers = new List<Customer>()
        //    {
        //        new Customer{Name = "Customer1"},
        //        new Customer{Name = "Customer2"}
        //    };
        //    //Instantiate a new VM
        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Customers = customers,
        //        Movie = movie
        //    };


        //    return View(viewModel);

        //}
        //public List<Movie> movies = new List<Movie>()
        //    {
        //        new Movie{Id = 1,Name = "Shrek"},
        //        new Movie{Id = 2,Name = "Wall-E"}
        //    };
        
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDAte(int year, int month)
        {
            return Content(year +"/" + month );
        }

        public ActionResult Edit(int id)
        {
            return Content("id= " + id);
        }

        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);
        }
        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = GetMovies().SingleOrDefault(p => p.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            return View(movie);

        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie{Id = 1,Name = "Shrek"},
                new Movie{Id = 2,Name = "Wall-E"}
            };
        } 
    }
}