using System.ComponentModel.DataAnnotations;
using FlightManagementBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly PostgresContext _context;

        public CustomerController(PostgresContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customer/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        
        //GET: api/Customer/email
        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email.ToLower().Equals(email.ToLower()));

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id:int}")]
        // public async Task<IActionResult> PutCustomer(int id, Customer customer)
        // {
        //     if (id != customer.Customerid)
        //     {
        //         return BadRequest();
        //     }
        //
        //     // _context.Entry(customer).State = EntityState.Modified;
        //     _context.Customers.Update(customer);
        //     
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!CustomerExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                if (CustomerExistsWithSameEmail(customer.Email))
                {
                    return Conflict("A customer with same email already exists!!");
                }
                else if(CustomerExistsWithSamePh(customer.Phone))
                {
                    return Conflict("A customer with same phone number already exists");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.Customerid }, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExistsWithSameEmail(string email)
        {
            return _context.Customers.Any(c => c.Email.ToLower().Equals(email.ToLower()));
        }

        private bool CustomerExistsWithSamePh(string ph)
        {
            return _context.Customers.Any(c => c.Phone.ToLower().Equals(ph.ToLower()));
        }
    }
}
