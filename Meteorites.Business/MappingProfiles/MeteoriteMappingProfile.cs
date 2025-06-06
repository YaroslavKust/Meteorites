using AutoMapper;
using Meteorites.Business.Models;
using Meteorites.DataAccess.Models;
using Meteorites.Infrastructure.Models;

namespace Meteorites.Business.MappingProfiles
{
    public class MeteoriteMappingProfile : Profile
    {
        public MeteoriteMappingProfile() 
        {
            CreateMap<MeteoriteGroupData, MeteoriteCompositionData>();

            CreateMap<MeteoriteExternalData, Meteorite>()
                .ForMember(d => d.Year, opt => opt.MapFrom(s => s.Year.Year));
        }
    }
}
