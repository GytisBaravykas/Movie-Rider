using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRider.Data;
using MovieRider.Dtos;
using MovieRider.Models;

namespace MovieRider.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        
        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //GET /api/customers
        public IActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers.Include(c => c.MembershipType).ToList();

            if (!String.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(c => c.Name.Contains(query)).ToList();
            }
            var customerDtos = customersQuery.Select(_mapper.Map<Customer, CustomerDto>);
            
            /*var customerDtos = _context.Customers.Include(c => c.MembershipType).ToList().Select(_mapper.Map<Customer, CustomerDto>);*/

            return Ok(customerDtos);
        }

        //GET /api/customers/1
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<Customer,CustomerDto>(customer));
        }

        // POST /api/customers
        // To add "NotFound()" or NotFoundResult() should be a IActionResult CreateCustomer
        [HttpPost]
        [Authorize(Roles=RoleName.CanManageMovies)]
        public IActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(UriHelper.GetDisplayUrl(Request)+"/"+customer.Id), customerDto); 

        }

        // PUT /api/customers/1
        [HttpPut("{id}")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new Exception("Bad request");

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null) 
                throw new Exception("Not Found");

            _mapper.Map(customerDto,customerInDb);

            /*
            customerInDb.FirstName = customerDto.FirstName;
            customerInDb.LastName = customerDto.LastName;
            customerInDb.BirthDate = customerDto.BirthDate;
            customerInDb.IsSubscibedToNewsLetter = customerDto.IsSubscibedToNewsLetter;
            customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
            */

            _context.SaveChanges();
        }

        // DELETE /api/customer/1
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new Exception("Not Found");
           
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}