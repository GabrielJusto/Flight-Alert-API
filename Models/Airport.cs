using System.ComponentModel.DataAnnotations;

namespace Flight_Alert_API.Models;

public class Airport
{
    public int Id { get; set; } 
    public string Ident { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Name { get; set; } = null!;
    public double LatitudeDeg { get; set; } 
    public double LongitudeDeg { get; set; } 
    public int? ElevationFt { get; set; } 
    public string Continent { get; set; } = null!;
    public string IsoCountry { get; set; } = null!;
    public string IsoRegion { get; set; } = null!;
    public string Municipality { get; set; } = null!;
    public string ScheduledService { get; set; } = null!;
    public string IcaoCode { get; set; } = null!;
    public string IataCode { get; set; } = null!;
    public string GpsCode { get; set; } = null!;
    public string LocalCode { get; set; } = null!;
    public string? HomeLink { get; set; } = string.Empty;
    public string? WikipediaLink { get; set; } = string.Empty;
    public string? Keywords { get; set; } = string.Empty;
}
