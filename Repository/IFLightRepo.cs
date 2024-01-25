namespace FlightManagementBackend.Repository;

public interface IFLightRepo<TFlight> where TFlight: class
{
    Task<IEnumerable<TFlight>> GetAllTFlightsAsync();
    Task<TFlight> AddTFlightAsync(TFlight f);
    Task UpdateTFlightAsync(int id, TFlight f);
    Task<TFlight> GetTFlightByIdAsync(int id);
    Task DeleteTFlightAsync(int id);
}