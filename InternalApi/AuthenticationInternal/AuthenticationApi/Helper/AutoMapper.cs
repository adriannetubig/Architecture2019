using AuthenticationBusiness.Models;
using AuthenticationData.Entities;
using AutoMapper;

namespace AuthenticationApi.Helper
{
    public static class AutoMapper
    {
        public static IMapper Config()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
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
