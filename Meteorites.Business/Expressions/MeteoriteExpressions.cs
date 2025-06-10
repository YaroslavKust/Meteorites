using Meteorites.Business.Models;
using Meteorites.DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Meteorites.Business.Expressions
{
    public class MeteoriteExpressions
    {
        public static Expression<Func<Meteorite, bool>> ByFilter(Filter filter)
        {
            Expression<Func<Meteorite, bool>> specification = m =>
               (filter.StartYear.HasValue ? m.Year >= filter.StartYear : true) &&
               (filter.EndYear.HasValue ? m.Year <= filter.EndYear : true) &&
               (!filter.RecClass.IsNullOrEmpty() ? m.RecClass == filter.RecClass : true) &&
               (!filter.SearchQuery.IsNullOrEmpty() ? m.Name.Contains(filter.SearchQuery) : true);

            return specification;
        }
    }
}
