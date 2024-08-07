using AutoMapper;
using BP.BookingMS.Business.Models.Hotel;
using BP.BookingMS.Data.Entities;

namespace BP.BookingMS.Business.MappingProfiles
{
    internal class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<HotelCreateModel, HotelEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.LandlordId, opt => opt.Ignore())
                .ForMember(dest => dest.Landlord, opt => opt.Ignore());
        }
    }
}
