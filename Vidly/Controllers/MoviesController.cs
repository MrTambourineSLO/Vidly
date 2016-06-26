using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie(){Name = "Shrek"};
            //Create a list of cutomers
            var customers = new List<Customer>()
            {
                new Customer{Name = "Customer1"},
                new Customer{Name = "Customer2"}
            };
            //Instantiate a new VM
            var viewModel = new RandomMovieViewModel
            {
                Customers = customers,
                Movie = movie
            };


            return View(viewModel);

        }
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDAte(int year, int month)
        {
            return Content(year +"/" + month );
        }

        public ActionResult Edit(int id)
        {
            return Content("id= " + id);
        }

        public ActionResult Index(int? pageIndex,string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }
            return Content(String.Format("Page index: " + pageIndex + "SortBy= " + sortBy));
        }
    }
}