﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<CustomerDTO> GetCustomers()
        {
            //Don't forget to cast to list
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDTO>);
        }
        //GET /api/customers/id

        public CustomerDTO GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            //Check if id was out of range
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Mapper.Map<Customer,CustomerDTO>(customer);
        }

        //POST /api/customers
        // We return newly created resource by convention to client
        [HttpPost]
        public CustomerDTO CreateCustomer(CustomerDTO customerDto)
        {
            //is model state valid?
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var customer = Mapper.Map<CustomerDTO, Customer>(customerDto);
            _context.Customers.Add(customer);
            //Now customer has an auto generated id
            _context.SaveChanges();
            //Add id that was generate by the database
            customerDto.Id = customer.Id;
            return customerDto;
        }
        //POST /api/customers/1 
        //Essentially edit customer
        //We can either return a customer or void it's all the same
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDTO customerDto)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            //Was there a bad id?
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            //Map two customers
            Mapper.Map(customerDto,customerInDb);

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
