using AutoMapper;
using BP.AccountsMS.Data.Entities;
using GrpcContracts;

namespace BP.AccountsMS.Business.MappingProfiles
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserAccountRequest, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => false));
        }
    }
}
