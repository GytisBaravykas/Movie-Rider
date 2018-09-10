using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRider.Data;
using MovieRider.Models;
using MovieRider.ViewModel;

namespace MovieRider.Controllers
{
    //[Authorize(Roles ="CanManageMovies")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("Index");

            return View("ReadOnlyList");
        }

        public IActionResult Details(int id)
        {
            var customers = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customers == null)
                return NotFound();

            return View(customers);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [Authorize(Roles=RoleName.CanManageMovies)]
        public IActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm",viewModel);
            }
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.FirstName = customer.FirstName;
                customerInDb.LastName = customer.LastName;
                customerInDb.MembershipTypeId = customer.MembershipTypeId; 
                customerInDb.IsSubscibedToNewsLetter = customer.IsSubscibedToNewsLetter;
                //Mapper.Map(customer, customerInDb) Auto generates fields up above
                //TryValidateModel(customerInDb,"", new string[] {"Name","Email"});
            }

            _context.SaveChanges();

            return RedirectToAction("Index","Customers");
        }
    }
}