using Meteorites.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
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

            filteredMeteorites = Order(filteredMeteorites, orderBy, descending);

            return await filteredMeteorites.ToListAsync();
        }

        public async Task UpdateMeteorites(IReadOnlyList<Meteorite> meteorites)
        {
            var existingMeteorites = context.Meteorites.ToDictionary(m => m.Id);
            var externalMeteorites = meteorites.ToDictionary(m => m.Id);

            var removedMeteorites = existingMeteorites
                .Where(m => !externalMeteorites.ContainsKey(m.Key))
                .Select(m => m.Value);

            var addedMeteorites = externalMeteorites
                .Where(m => !existingMeteorites.ContainsKey(m.Key))
                .Select(m => m.Value);

            var updatedMeteorites = externalMeteorites
                .Where(m => existingMeteorites.ContainsKey(m.Key))
                .Select(m => m.Value);

            foreach(var meteorite in updatedMeteorites)
            {
                var existing = externalMeteorites[meteorite.Id];

                existing.Latitude = meteorite.Latitude;
                existing.Longitude = meteorite.Longitude;
                existing.Year = meteorite.Year;
                existing.RecClass = meteorite.RecClass;
                existing.Name = meteorite.Name;
                existing.NameType = meteorite.NameType;
                existing.Fall = meteorite.Fall;
                existing.Mass = meteorite.Mass;
                existing.Geolocation = meteorite.Geolocation;
                existing.ComputedRegionCbhkFwbd = meteorite.ComputedRegionCbhkFwbd;
                existing.ComputedRegionNnqa25F4 = meteorite.ComputedRegionNnqa25F4;
            }

            await context.AddRangeAsync(addedMeteorites);

            context.RemoveRange(removedMeteorites);

            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<string>> GetRecClasses()
        {
            var classes = await context.Meteorites
                .Select(m => m.RecClass)
                .Distinct()
                .ToListAsync();

            return classes;
        }

        public async Task<IReadOnlyList<int>> GetYears()
        {
            var classes = await context.Meteorites
                .Select(m => m.Year)
                .Distinct()
                .Order()
                .ToListAsync();

            return classes;
        }

        private IQueryable<MeteoriteGroupData> Order(IQueryable<MeteoriteGroupData> meteorites, string orderBy, bool descending)
        {
            var sortableProperties = typeof(MeteoriteGroupData).GetProperties().Select(p => p.Name);

            if (sortableProperties.Contains(orderBy))
            {
                meteorites = descending
                    ? meteorites.OrderBy($"{orderBy} DESC")
                    : meteorites.OrderBy(orderBy);
            }

            return meteorites;
        }
    }
}
