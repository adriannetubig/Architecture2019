using AutoMapper;
using BaseData.Entities;
using BaseModel;
using ErrorLoggerBusiness.Models;
using ErrorLoggerData.Entities;
using System;

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
                mc.CreateMap<EntityPagedList<EntityExceptionLog>, PagedList<ExceptionLog>>();
                mc.CreateMap<Exception, ExceptionLog>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}