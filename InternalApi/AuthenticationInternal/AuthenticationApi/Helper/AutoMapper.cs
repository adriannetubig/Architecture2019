using AuthenticationBusiness.Models;
using AuthenticationData.Entities;
using AutoMapper;

namespace AuthenticationApi.Helper
{
    public class AutoMapper
    {
        public static IMapper Config()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<EntityFibonacci, Fibonacci>();
                mc.CreateMap<Fibonacci, EntityFibonacci>();
                mc.CreateMap<EntityRefreshToken, RefreshToken>();
                mc.CreateMap<EntityRole, Role>();
                mc.CreateMap<EntityUser, User>();
                mc.CreateMap<User, EntityUser>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
