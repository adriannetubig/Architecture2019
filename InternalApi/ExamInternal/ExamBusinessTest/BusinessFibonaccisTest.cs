using AutoMapper;
using BaseData.Interfaces;
using BaseModel;
using ExamBusiness.Interfaces;
using ExamBusiness.Models;
using ExamBusiness.Services;
using ExamData.Entities;
using ExamData.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ExamBusinessTest
{
    public class BusinessFibonaccisTest
    {
        private IMapper _imapper;
        private IBusinessFibonaccis _iBusinessFibonaccis;

        Mock<IDataFibonaccis> _moqDataFibonaccis;
        Mock<IRepoBase> _moqRepoBase;

        private readonly EntityFibonacci _entityFibonacci;
        private readonly Fibonacci _fibonacci;
        private PageFilter _pageFilter;

        public BusinessFibonaccisTest()
        {
            _moqDataFibonaccis = new Mock<IDataFibonaccis>();
            _moqRepoBase = new Mock<IRepoBase>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<EntityFibonacci, Fibonacci>();
                mc.CreateMap<Fibonacci, EntityFibonacci>();
            });
            _imapper = mappingConfig.CreateMapper();

            _entityFibonacci = new EntityFibonacci
            {
                Iterations = 1,
                Total = 1
            };

            _fibonacci = new Fibonacci
            {
                Iterations = 1,
                Total = 1
            };
        }

        [Fact]
        public async Task Create_TestIfCorrectTotalAsync()
        {
            _iBusinessFibonaccis = new BusinessFibonaccis(_imapper, _moqRepoBase.Object, _moqDataFibonaccis.Object);
            var requestResult = await _iBusinessFibonaccis.Create(_fibonacci, default);

            Assert.Equal(1, requestResult.Model.Total);
        }
    }
}
