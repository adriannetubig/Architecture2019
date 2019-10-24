using AutoMapper;
using BaseModel;
using ErrorLoggerBusiness.Models;
using ErrorLoggerData.Entities;

namespace ErrorLoggerApi.Helper
{
    public static class AutoMapper
    {
        public static IMapper Config()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<ExceptionLog, EntityExceptionLog>().ReverseMap();
                mc.CreateMap<InnerExceptionLog, EntityInnerExceptionLog>().ReverseMap();
                mc.CreateMap<PagedList<ExceptionLog>, PagedList<EntityExceptionLog>>().ReverseMap();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}