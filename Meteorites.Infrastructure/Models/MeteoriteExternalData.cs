using System.Text.Json.Serialization;
using Meteorites.Domain.Models;

namespace Meteorites.Infrastructure.Models;

public class MeteoriteExternalData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NameType { get; set; }
    public string RecClass { get; set; }
    public int Mass { get; set; }
    public string Fall { get; set; }
    public DateTime Year { get; set; }

    [JsonPropertyName("reclat")]
    public double Latitude { get; set; }

    [JsonPropertyName("reclong")]
    public double Longitude { get; set; }

    public GeoLocation Geolocation { get; set; }

    [JsonPropertyName(":@computed_region_cbhk_fwbd")]
    public string ComputedRegionCbhkFwbd { get; set; }

    [JsonPropertyName(":@computed_region_nnqa_25f4")]
    public string ComputedRegionNnqa25F4 { get; set; }
}
