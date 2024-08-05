using AutoMapper;
using BP.BookingMS.Data.Entities;
using GrpcContracts;

namespace BP.BookingMS.Business.MappingProfiles
{
    internal class TenantMappingProfile : Profile
    {
        public TenantMappingProfile()
        {
            CreateMap<CreatePartyRequest, TenantEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
