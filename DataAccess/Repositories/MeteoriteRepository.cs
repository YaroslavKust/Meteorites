using Meteorites.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Meteorites.DataAccess.Repositories
{
    public class MeteoriteRepository(MeteoriteDbContext context) : IMeteoriteRepository
    {
        public async Task<IReadOnlyList<MeteoriteGroupData>> GetGroupedMeteorites(
            Expression<Func<Meteorite, bool>> filterFunc,
            string orderBy,
            bool descending = false)
        {
            var filteredMeteorites = context.Meteorites
                .Where(filterFunc)
                .GroupBy(m => m.Year)
                .Select(g => new MeteoriteGroupData 
                    { 
                        Year = g.Key,
                        Count = g.Count(),
                        TotalMass = g.Sum(m => m.Mass)
                    });

            Order(filteredMeteorites, orderBy, descending);

            return await filteredMeteorites.ToListAsync();
        }

        public async Task UpdateMeteorites(IReadOnlyList<Meteorite> meteorites)
        {
            context.UpdateRange(meteorites);

            await context.SaveChangesAsync();
        }

        private void Order(IQueryable<MeteoriteGroupData> meteorites, string orderBy, bool descending)
        {
            var sortableProperties = typeof(MeteoriteGroupData).GetProperties().Select(p => p.Name);

            if (sortableProperties.Contains(orderBy))
            {
                meteorites = descending
                    ? meteorites.OrderByDescending(_ => orderBy)
                    : meteorites.OrderBy(_ => orderBy);
            }
        }
    }
}
