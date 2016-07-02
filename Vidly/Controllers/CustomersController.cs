using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
      // GET: Customers
        //Route Customers only
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
           return View();
        }

        [Route("customers/Details/{id}")]
        public ActionResult Details(int id)
        {
            //var customer = _context.Customers.SingleOrDefault(p => p.Id == id);
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(p => p.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
       } 
        public ActionResult New()
        {
            var memberships = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = memberships
            
            };
            return View("CustomerForm",viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            else if (customer.Id == 0)
            {
                _context.Customers.Add(customer);    
            }
            else

            {
                var customerToUpdate = _context.Customers.Single(c => c.Id == customer.Id);
                customerToUpdate.Name = customer.Name;
                customerToUpdate.MembershipTypeId = customer.MembershipTypeId;
                customerToUpdate.Birthday = customer.Birthday;
                customerToUpdate.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            
            return View("CustomerForm",viewModel);
        }
    }
}