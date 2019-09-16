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
                mc.CreateMap<EntityRole, Role>();
                mc.CreateMap<EntityUser, User>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
