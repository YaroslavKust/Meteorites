using Meteorites.Infrastructure.Constants;
using Meteorites.Infrastructure.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Meteorites.Infrastructure.ExternalClients;

public class MeteoriteExternalClient(HttpClient httpClient) : IMeteoriteExternalClient
{
    public async Task<IReadOnlyList<MeteoriteExternalData>> GetMeteorites()
    {
        try
        {
            var data = await httpClient.GetFromJsonAsync<IReadOnlyList<MeteoriteExternalData>>(ExternalUrl.Meteorites);

            return data;
        }
        catch (HttpRequestException httpException) 
        {
            Console.WriteLine($"Network Error: {httpException.Message}");
        }
        catch (JsonException jsonException)
        {
            Console.WriteLine($"Deserealizing Error: {jsonException.Message}");
        }

        return null;
    }
}
