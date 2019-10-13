using AutoMapper;
using BaseData.Interfaces;
using ExamBusiness.Interfaces;
using ExamBusiness.Models;
using ExamBusiness.Services;
using ExamData.Entities;
using ExamData.Interfaces;
using Moq;
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

        private readonly Fibonacci _fibonacci;

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

            _fibonacci = new Fibonacci
            {
                Iterations = 1,
                Total = 0
            };
        }

        [Theory]
        [InlineData(true, 0, 0)]
        [InlineData(true, 1, 1)]
        [InlineData(true, 2, 1)]
        [InlineData(true, 3, 2)]
        [InlineData(true, 46, 1836311903)]
        [InlineData(false, 47, 0)]
        public async Task Create_TestIfCorrectTotal(bool suceeded, int iterations, int total)
        {
            _iBusinessFibonaccis = new BusinessFibonaccis(_imapper, _moqRepoBase.Object, _moqDataFibonaccis.Object);
            _fibonacci.Iterations = iterations;
            var requestResult = await _iBusinessFibonaccis.Create(_fibonacci, default);

            Assert.Equal(total, requestResult.Model.Total);
            Assert.Equal(suceeded, requestResult.Succeeded);
        }

        [Fact]
        public async Task Create_TestIfErrorWillShowBeyond46()
        {
            _iBusinessFibonaccis = new BusinessFibonaccis(_imapper, _moqRepoBase.Object, _moqDataFibonaccis.Object);
            _fibonacci.Iterations = 47;
            var requestResult = await _iBusinessFibonaccis.Create(_fibonacci, default);

            Assert.Contains("Cannot compute for more than 46", requestResult.Errors);
            Assert.False(requestResult.Succeeded);
        }
    }
}
