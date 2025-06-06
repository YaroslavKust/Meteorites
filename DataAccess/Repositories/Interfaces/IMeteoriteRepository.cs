using Meteorites.DataAccess.Models;
using System.Linq.Expressions;

namespace Meteorites.DataAccess.Repositories
{
    public interface IMeteoriteRepository
    {
        Task<IReadOnlyList<MeteoriteGroupData>> GetGroupedMeteorites(Expression<Func<Meteorite,bool>> filterFunc, string orderBy, bool descending);

        Task UpdateMeteorites(IReadOnlyList<Meteorite> meteorites);
    }
}
