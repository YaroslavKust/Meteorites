using Meteorites.Domain.Models;

namespace Meteorites.DataAccess.Models
{
    public class Meteorite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
        public string RecClass { get; set; }
        public int Mass { get; set; }
        public string Fall { get; set; }
        public int Year { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public GeoLocation Geolocation { get; set; }
        public string ComputedRegionCbhkFwbd { get; set; }
        public string ComputedRegionNnqa25F4 { get; set; }
    }
}
