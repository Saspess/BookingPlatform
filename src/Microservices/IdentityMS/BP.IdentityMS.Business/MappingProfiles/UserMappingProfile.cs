using AutoMapper;
using BP.AuthProvider.Models;
using BP.IdentityMS.Business.Commands.User;
using BP.IdentityMS.Business.Enums;
using BP.IdentityMS.Data.Entities;
using BP.Utils.Helpers;
using GrpcContracts;

namespace BP.IdentityMS.Business.MappingProfiles
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegisterCommand, UserEntity>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHelper.HashPassword(src.Password, PasswordHelper.GenerateSalt())))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.GetName(typeof(Role), src.Role).ToString()));

            CreateMap<UserEntity, AuthModel>();

            CreateMap<UserRegisterCommand, CreateUserAccountRequest>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Enum.GetName(typeof(Role), src.Role).ToString()));
        }
    }
}
