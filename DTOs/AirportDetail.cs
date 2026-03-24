
using Flight_Alert_API.Models;

namespace Flight_Alert_API.DTOs;

public class AirportDetail(Airport airport)
{
    public string Name { get; set; } = airport.Name;
    public string IataCode { get; set; } = airport.IataCode!;
    public string City { get; set; } = airport.Municipality;

}