
using Flight_Alert_API.DTOs.Flight;

namespace Flight_Alert_API.Services.Interfaces;

public interface ISerpGoogleFlightsService
{
    public List<FlightSearchResult> GetFlights(SerpGoogleFlightsRequest request);
    public Task<List<BookingOptionDto>> GetBookingOptionsAsync(SerpGoogleFlightsRequest request);
}