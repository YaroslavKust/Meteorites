using Meteorites.Business.Models;
using Meteorites.DataAccess.Models;
using System.Linq.Expressions;

namespace Meteorites.Business.Expressions
{
    public class MeteoriteExpressions
    {
        public static Expression<Func<Meteorite, bool>> ByFilter(Filter filter)
        {
            Expression<Func<Meteorite, bool>> specification = m =>
               m.Year >= filter.StartYear &&
               m.Year <= filter.EndYear &&
               m.RecClass == filter.RecClass &&
               m.Name.Contains(filter.SearchQuery);

            return specification;
        }
    }
}
