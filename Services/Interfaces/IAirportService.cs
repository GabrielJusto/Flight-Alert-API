using Flight_Alert_API.DTOs;

namespace Flight_Alert_API.Services.Interfaces;

public interface IAirportService
{
    public Task<List<AirportDetail>> GetAirportsAsync();
}