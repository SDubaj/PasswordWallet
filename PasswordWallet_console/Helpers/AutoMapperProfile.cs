using AutoMapper;
using PasswordWallet_console.Entities;
using PasswordWallet_console.Models.DataChange;
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
            CreateMap<UpdatePasswordModel, Password>();
            CreateMap<functionRun, functionRunModel>();
            CreateMap<functionRunModel, functionRun>();
            CreateMap<FunctionType, FunctionModel>();
            CreateMap<ActionTypeModel, ActionType>();
            CreateMap<FunctionModel, FunctionType>();
            CreateMap<DataChangeModel, DataChange>();
            CreateMap<DataChange, DataChangeModel>();
        }
    }
}