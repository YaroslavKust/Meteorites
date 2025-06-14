﻿using Meteorites.Business.Models;

namespace Meteorites.Business.Services
{
    public interface IMeteoriteService
    {
        Task<IReadOnlyList<MeteoriteCompositionData>> GetMeteoritesData(Filter filter, OrderingOptions orderingOptions);
        Task UpdateMeteorites();

        Task<IReadOnlyList<string>> GetRecClasses();
        Task<IReadOnlyList<int>> GetYears();
    }
}
