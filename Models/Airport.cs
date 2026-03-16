using System.ComponentModel.DataAnnotations;

namespace Flight_Alert_API.Models
{
    public class Airport
    {
        public int Id { get; set; } 
        public string Ident { get; set; } 
        public string Type { get; set; } 
        public string Name { get; set; } 
        public double LatitudeDeg { get; set; } 
        public double LongitudeDeg { get; set; } 
        public int? ElevationFt { get; set; } 
        public string Continent { get; set; } 
        public string IsoCountry { get; set; } 
        public string IsoRegion { get; set; } 
        public string Municipality { get; set; }
        public string ScheduledService { get; set; } 
        public string IcaoCode { get; set; } 
        public string IataCode { get; set; } 
        public string GpsCode { get; set; } 
        public string LocalCode { get; set; } 
        public string HomeLink { get; set; } 
        public string WikipediaLink { get; set; } 
        public string Keywords { get; set; } 
    }
}
