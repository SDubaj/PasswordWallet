using AutoMapper;
using PasswordWallet_console.Entities;
using PasswordWallet_console.Models.Passwords;
using PasswordWallet_console.Models.Users;

namespace PasswordWallet_console.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<Password, PasswordModel>();
            CreateMap<PasswordModel, Password>();
        }
    }
}