using AutoMapper;
using BP.AccountsMS.Business.Models.Account;
using BP.AccountsMS.Data.Entities;
using GrpcContracts;

namespace BP.AccountsMS.Business.MappingProfiles
{
    internal class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<CreateUserAccountRequest, AccountEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => false));

            CreateMap<AccountEntity, AccountViewModel>();
        }
    }
}
