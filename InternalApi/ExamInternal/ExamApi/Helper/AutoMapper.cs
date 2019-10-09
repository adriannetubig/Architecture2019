using AutoMapper;
using ExamBusiness.Models;
using ExamData.Entities;

namespace ExamApi.Helper
{
    public static class AutoMapper
    {
        public static IMapper Config()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<EntityFibonacci, Fibonacci>().ReverseMap();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
