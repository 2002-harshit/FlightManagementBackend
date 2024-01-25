using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightManagementBackend.Models;

namespace FlightManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingdetailController : ControllerBase
    {
        private readonly PostgresContext _context;

        public BookingdetailController(PostgresContext context)
        {
            _context = context;
        }

        // GET: api/Bookingdetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookingdetail>>> GetBookingdetails()
        {
            return await _context.Bookingdetails.ToListAsync();
        }

        // GET: api/Bookingdetail/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Bookingdetail>> GetBookingdetail(int id)
        {
            var bookingdetail = await _context.Bookingdetails.FindAsync(id);

            if (bookingdetail == null)
            {
                return NotFound();
            }

            return bookingdetail;
        }

        // PUT: api/Bookingdetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingdetail(int id, Bookingdetail bookingdetail)
        {
            if (id != bookingdetail.Bookingid)
            {
                return BadRequest();
            }

            _context.Entry(bookingdetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingdetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookingdetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookingdetail>> PostBookingdetail(Bookingdetail bookingdetail)
        {
            _context.Bookingdetails.Add(bookingdetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookingdetailExists(bookingdetail.Bookingid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBookingdetail", new { id = bookingdetail.Bookingid }, bookingdetail);
        }

        // DELETE: api/Bookingdetail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingdetail(int id)
        {
            var bookingdetail = await _context.Bookingdetails.FindAsync(id);
            if (bookingdetail == null)
            {
                return NotFound();
            }

            _context.Bookingdetails.Remove(bookingdetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingdetailExists(int id)
        {
            return _context.Bookingdetails.Any(e => e.Bookingid == id);
        }
    }
}
