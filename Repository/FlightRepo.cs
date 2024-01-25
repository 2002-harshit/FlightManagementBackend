using FlightManagementBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementBackend.Repository;

public class FlightRepo:IFLightRepo<Flight>
{
    private readonly PostgresContext _context;

    public FlightRepo(PostgresContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Flight>> GetAllTFlightsAsync()
    {
        try
        {
            return await _context.Flights.ToListAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.Error.WriteLine("Got error in db updation" + ex);
            throw;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Got error in repo layer" + ex);
            throw;
        }
    }

    public async Task<Flight> AddTFlightAsync(Flight f)
    {
        try
        {
            _context.Flights.Add(f);
            await _context.SaveChangesAsync();
            return f;
        }
        catch (DbUpdateException ex)
        {
            Console.Error.WriteLine("Got error in db updation" + ex);
            throw;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Got error in repo layer" + ex);
            throw;
        }
    }

    public async Task UpdateTFlightAsync(int id, Flight f)
    {
        try
        {
            Flight? existingFlight = await _context.Flights.FindAsync(id);
            if (existingFlight == null) throw new InvalidOperationException("Flight not found");

            // _context.Flights.Update(f);
            _context.Entry(existingFlight).CurrentValues.SetValues(f);
            await _context.SaveChangesAsync();

        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine("Got err in db layer, flight not found");
            throw;
        }
        catch (DbUpdateException ex)
        {
            Console.Error.WriteLine("Got error in db updation" + ex);
            throw;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Got error in repo layer" + ex);
            throw;
        }
    }

    public async Task<Flight> GetTFlightByIdAsync(int id)
    {
        try
        {
            Flight? existingFlight=await _context.Flights.FindAsync(id);
            if (existingFlight == null) throw new InvalidOperationException("Flight not found");

            return existingFlight;
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine("Got err in db layer, flight not found");
            throw;
        }
        catch (DbUpdateException ex)
        {
            Console.Error.WriteLine("Got error in db updation" + ex);
            throw;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Got error in repo layer" + ex);
            throw;
        }
    }

    public async Task DeleteTFlightAsync(int id)
    {
        try
        {
            Flight? existingFlight=await _context.Flights.FindAsync(id);
            if (existingFlight == null) throw new InvalidOperationException("Flight not found");

            _context.Flights.Remove(existingFlight);
            await _context.SaveChangesAsync();
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine("Got err in db layer, flight not found");
            throw;
        }
        catch (DbUpdateException ex)
        {
            Console.Error.WriteLine("Got error in db updation" + ex);
            throw;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Got error in repo layer" + ex);
            throw;
        }
    }
}