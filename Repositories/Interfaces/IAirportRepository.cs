
using Flight_Alert_API.Models;

namespace Flight_Alert_API.Repositories.Interfaces;

public interface IAirportRepository
{
    public Task<Airport?> GetByIATACodeAsync(string iataCode);
}