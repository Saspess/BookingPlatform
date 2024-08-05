using AutoMapper;
using BP.BookingMS.Data.Entities;
using GrpcContracts;

namespace BP.BookingMS.Business.MappingProfiles
{
    internal class LandlordMappingProfile : Profile
    {
        public LandlordMappingProfile()
        {
            CreateMap<CreatePartyRequest, LandlordEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
