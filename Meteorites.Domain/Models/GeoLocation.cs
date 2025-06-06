namespace Meteorites.Domain.Models
{
    public class GeoLocation
    {
        public string Type { get; set; }
        public required double[] Coordinates { get; set; }
    }
}
