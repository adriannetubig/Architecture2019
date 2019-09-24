using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using AuthenticationBusiness.Services;
using AuthenticationData.Entities;
using AuthenticationData.Interfaces;
using AutoMapper;
using BaseData.Interfaces;
using Moq;
using Xunit;

namespace AuthenticationBusinessTest
{
    public class BusinessUsersTest
    {
        private IBusinessUsers _iBusinessUsers;

        Mock<IDataUsers> _moqDataUsers;
        Mock<IRepoBase> _moqRepoBase;

        public BusinessUsersTest()
        {
            _moqDataUsers = new Mock<IDataUsers>();
            _moqRepoBase = new Mock<IRepoBase>();

            //ToDo: Check there is a better way on injecting the mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<EntityUser, User>();
                mc.CreateMap<User, EntityUser>();
            });

            IMapper mapper = mappingConfig.CreateMapper();

            _iBusinessUsers = new BusinessUsers(mapper, _moqRepoBase.Object, _moqDataUsers.Object);
        }

        [Fact]
        public void Create_DifferentScenarios()
        {

        }
    }
}
