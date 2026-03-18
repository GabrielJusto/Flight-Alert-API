
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Repositories.Implementations;

public class AirportRepository(
    AppDbContext context,
    ILogger<AirportRepository> logger
) : IAirportRepository
{

    private readonly AppDbContext _context_ = context;
    private readonly ILogger<AirportRepository> _logger = logger;
    public async Task<Airport?> GetByIATACodeAsync(string iataCode)
    {
        try
        {
            return await _context_.Airports.FirstOrDefaultAsync(a => a.IataCode == iataCode);
        }catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching airport with IATA code {IataCode}", iataCode);
            throw;
        }
    }
}