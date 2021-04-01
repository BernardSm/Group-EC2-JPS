using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCore.Model;

namespace WebApiCore.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    public class CustomersController : Controller
    {
       private NCBDataContext _context;

        public CustomersController(NCBDataContext context)
        {
            _context = context;
        }

        [System.Web.Http.HttpGet]
        public List<Customer> Get()
        {
            return _context.Customer.ToList();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("{Id}")]
        public Customer GetCustomer(int Id)
        {
            var customer = _context.Customer.Where(a => a.Id == Id).SingleOrDefault();
            return customer;
        }

        [System.Web.Http.HttpPost]

        public IActionResult PostCustomer([System.Web.Http.FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            _context.Customer.Add(customer);
            _context.SaveChanges();

            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if(customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
       
    }
}