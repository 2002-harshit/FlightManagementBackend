using FlightManagementBackend.Models;
using FlightManagementBackend.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FlightManagementBackend.Service;

public class FlightServ : IFlightServ<Flight>
{
    private readonly IFLightRepo<Flight> _flightRepo;
    public FlightServ(IFLightRepo<Flight> flightRepo)
    {
        _flightRepo = flightRepo;
    }


    public async Task<IEnumerable<Flight>> GetAllTFlightsAsync()
    {
        try
        {
            return await _flightRepo.GetAllTFlightsAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Got error in service layer "+e);
            throw;
        }
    }

    public async Task<Flight> AddTFlightAsync(Flight f)
    {
        try
        {
            if ((await _flightRepo.GetAllTFlightsAsync()).Any(eachFlight => eachFlight.Code == f.Code))
            {
                throw new ArgumentException("A flight with the same code already exists");
            }

            return await _flightRepo.AddTFlightAsync(f);

        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateTFlightAsync(int id, Flight f)
    {
        if (id != f.Flightid)
        {
            throw new ArgumentException("Flight ID mismatch");
        }
        

        
        if ((await _flightRepo.GetAllTFlightsAsync()).Any(eachFlight => eachFlight.Flightid!=id   && eachFlight.Code == f.Code))
        {
            throw new ArgumentException("A flight with the same code already exists");
        }

        try
        {
            await _flightRepo.UpdateTFlightAsync(id, f);
        }
        catch (ArgumentException e)
        {
            Console.Error.WriteLine("Got error in service layer " + e);
            throw;
        }
        catch (InvalidOperationException e)
        {
            Console.Error.WriteLine("Got error in service layer " + e);
            throw;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Got error in service layer " + e);
            throw;
        }
    }

    public async Task<Flight> GetTFlightByIdAsync(int id)
    {
        try
        {
            return await _flightRepo.GetTFlightByIdAsync(id);
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine("Got err in db layer, flight not found");
            throw;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine("Got error in service layer "+e);
            throw;
        }
    }

    public async Task DeleteTFlight(int id)
    {
        try
        {
            await _flightRepo.DeleteTFlightAsync(id);
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}