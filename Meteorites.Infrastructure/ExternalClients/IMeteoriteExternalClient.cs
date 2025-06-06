using Meteorites.Infrastructure.Models;

namespace Meteorites.Infrastructure.ExternalClients
{
    public interface IMeteoriteExternalClient
    {
        Task<IReadOnlyList<MeteoriteExternalData>> GetMeteorites();
    }
}
