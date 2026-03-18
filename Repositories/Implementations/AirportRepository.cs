
using Flight_Alert_API.Database;
using Flight_Alert_API.Models;
using Flight_Alert_API.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Flight_Alert_API.Repositories.Implementations;

public class AirportRepository(
    AppDbContext context
) : IAirportRepository
{

    private readonly AppDbContext _context_ = context;
    public async Task<Airport?> GetByIATACodeAsync(string iataCode)
    {
        return await _context_.Airports.FirstOrDefaultAsync(a => a.IataCode == iataCode);
    }
}