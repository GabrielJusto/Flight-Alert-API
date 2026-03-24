
using Flight_Alert_API.DTOs;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;
using Flight_Alert_API.Services.Interfaces;

namespace Flight_Alert_API.Services.implemetations;

public class AirportService(
    IAirportRepository airportRepository,
    ILogger<AirportService> logger
) : IAirportService
{

    private readonly IAirportRepository _airportRepository = airportRepository;
    private readonly ILogger _logger = logger;
    public async Task<List<AirportDetail>> GetAirportsAsync()
    {
        try
        {
            List<Airport> airports = await _airportRepository.GetAllAsync();
            return airports.Select(a => new AirportDetail(a)).ToList();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error geting all airports datail.");
            throw;
        }
    }
}