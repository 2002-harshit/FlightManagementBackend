namespace FlightManagementBackend.Service;

public interface IFlightServ<TFlight>
{
    Task<IEnumerable<TFlight>> GetAllTFlightsAsync();
    Task<TFlight> AddTFlightAsync(TFlight f);
    Task UpdateTFlightAsync(int id, TFlight f);
    Task<TFlight> GetTFlightByIdAsync(int id);
    Task DeleteTFlight(int id);
}