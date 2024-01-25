using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightManagementBackend.Models;
using FlightManagementBackend.Service;

namespace FlightManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        // private readonly PostgresContext _context;
        private readonly IFlightServ<Flight> _flightServ;
        
        public FlightController(IFlightServ<Flight> flightServ)
        {
            _flightServ = flightServ;
        }

        // GET: api/Flight
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            try
            {
                return Ok(await _flightServ.GetAllTFlightsAsync());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving flights from database");
            }
        }

        // GET: api/Flight/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            try
            {
                return await _flightServ.GetTFlightByIdAsync(id);

            }
            catch (InvalidOperationException ex)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Got error in Controller layer "+e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving flights from database");
            }


        }

        // PUT: api/Flight/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            try
            {
                await _flightServ.UpdateTFlightAsync(id, flight);
                return NoContent();
            }
            catch (ArgumentException e)
            {
                if (e.Message.ToLower().Contains("id".ToLower()))
                {
                    return BadRequest();
                }
                else
                {
                    return Conflict("A flight with same code already exists!!");
                }
            }
            catch (InvalidOperationException e)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Got error in Controller layer "+e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving flights from database");
            }
        }

        // POST: api/Flight
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(Flight flight)
        {
            try
            {
                var newFlight=await _flightServ.AddTFlightAsync(flight);
                return CreatedAtAction("GetFlight",new {id=newFlight.Flightid},newFlight);
            }
            catch (ArgumentException)
            {
                return Conflict("A flight with same code already exists!!");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Got error in Controller layer "+e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving flights from database");
            }
        }

        // DELETE: api/Flight/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            try
            {
                await _flightServ.DeleteTFlight(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Got error in Controller layer "+e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving flights from database");
            }
        }
    }
}
