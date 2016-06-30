using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<Customer> GetCustomers()
        {
            //Don't forget to cast to list
            return _context.Customers.ToList();
        }
        //GET /api/customers/id

        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            //Check if id was out of range
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customer;
        }

        //POST /api/customers
        // We return newly created resource by convention to client
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            //is model state valid?
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _context.Customers.Add(customer);
            //Now customer has an auto generated id
            _context.SaveChanges();

            return customer;
        }
        //POST /api/customers/1 
        //Essentially edit customer
        //We can either return a customer or void it's all the same
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            //Was there a bad id?
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //Edit customer info:
            customerInDb.Birthday = customer.Birthday;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.Name = customer.Name;

            _context.SaveChanges();
        }
        //DELETE /api/customers/id
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
        
        
    }
}

