using AutoMapper;
using BP.IdentityMS.Business.Commands.User;
using BP.IdentityMS.Data.Entities;

namespace BP.IdentityMS.Business.MappingProfiles
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterCommand, UserEntity>();
        }
    }
}
