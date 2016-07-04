using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult NewRental(RentalDto rentalDto)
        {
            //Single and NOT SingleOrDefault because we trust that the 
            //operator will send us correct customerId (since he will probably
            //pick it from dropdown list or something - ALSO malicious user
            //who'd send invalid customerId would get some sort of internal server
            //error

            //If we were building PUBLIC API we would use Single and then check
            //if Customer == null and return a BadRequest("Invalid customer id");
            var customer = _context.Customers.Single(c => c.Id == rentalDto.CustomerId);
            
            /*
                Insted of foreach loop we could Use:
             * var movies = _context.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id);
             * This translates to SQL as:
             * SELECT *
             * FROM Movies
             * WHERE Id IN(1,2,3)
             */
            foreach (var movieId in rentalDto.MovieIds)
            {
                var movie = _context.Movies.Single(m => m.Id == movieId);
                {
                    Rental curRental = new Rental()
                    {
                        DateRented = DateTime.Now,
                        Customer = customer,
                        Movie = movie
                    };
                    _context.Rentals.Add(curRental);
                    
                }
                //It's most efficient if SaveChanges() is outside the Loop
                _context.SaveChanges();
            }
            //We use return Ok(); because we didn't create a SINGLE(as opposed to none OR
            // MULTIPLE)new object, in this
            //case we'd have to provide URI to newly created resource, in this case
            //we have mutiple resources
            //return Created(new Uri(Request.RequestUri + "/" + rentalDto.Id), rentalDto);
            return Ok();
        }
    }
}
