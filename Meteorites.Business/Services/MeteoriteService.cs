using AutoMapper;
using Meteorites.Business.Expressions;
using Meteorites.Business.Models;
using Meteorites.DataAccess.Models;
using Meteorites.DataAccess.Repositories;
using Meteorites.Infrastructure.ExternalClients;
using Meteorites.Infrastructure.Models;

namespace Meteorites.Business.Services
{
    public class MeteoriteService(IMeteoriteExternalClient externalClient, IMeteoriteRepository repository, IMapper mapper) : IMeteoriteService
    {
        public async Task<IReadOnlyList<MeteoriteCompositionData>> GetMeteoritesData(Filter filter, OrderingOptions orderingOptions)
        {
            var result = await repository.GetGroupedMeteorites(
                MeteoriteExpressions.ByFilter(filter),
                orderingOptions.OrderBy,
                orderingOptions.Descending);

            return mapper.Map<IReadOnlyList<MeteoriteGroupData>, IReadOnlyList<MeteoriteCompositionData>>(result);
        }

        public async Task UpdateMeteorites()
        {
            var meteorites = await externalClient.GetMeteorites();

            var meteoritesData = mapper.Map<IReadOnlyList<MeteoriteExternalData>, IReadOnlyList<Meteorite>>(meteorites);



            await repository.UpdateMeteorites(meteoritesData);
        }
    }
}
