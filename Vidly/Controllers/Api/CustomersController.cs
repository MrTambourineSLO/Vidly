using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AutoMapper;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
            
        }
        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            //Don't forget to cast to list
            //We perform eager loading w/ include from Systam.Data.Entity
            var customerDtos = _context.Customers
                .Include(i => i.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer,CustomerDto>);
            return Ok(customerDtos);
        }
        //GET /api/customers/id

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            //Check if id was out of range
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/customers
        // We return newly created resource by convention to client
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            //is model state valid?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            //Now customer has an auto generated id
            _context.SaveChanges();
            //Add id that was generate by the database
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri +"/"+ customer.Id), customerDto );
        }
        //POST /api/customers/1 
        //Essentially edit customer
        //We can either return a customer or void it's all the same
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            //Was there a bad id?
            if (customerInDb == null)
            {
                return NotFound();
            }
            //Map two customers
            Mapper.Map(customerDto,customerInDb);
            _context.SaveChanges();


            return Ok();
        }
        //DELETE /api/customers/id
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
        //Input is Customer & Movie ID
        // Return simple response (don't worry about implementation just yet)
         // eg: /reviews/5
        //[HttpGet]
        //[Route("api/customers/rentmovie/{customerId}/{movieId}")]
        //public IHttpActionResult RentMovie(int customerId, int movieId)
        //{
        //    //Perform check if customerId is valid
        //    //Perform check if movieId is valid
        //    //Perform DB updates
        //    //Return customer info (just a mock example)
        //    var rentingCustomer = _context.Customers.SingleOrDefault(c => c.Id == customerId);
        //    return Ok(Mapper.Map<Customer,CustomerDto>(rentingCustomer));
        //}
        
        
    }
}

