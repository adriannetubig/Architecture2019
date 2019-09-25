using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using AuthenticationBusiness.Services;
using AuthenticationData.Entities;
using AuthenticationData.Interfaces;
using AutoMapper;
using BaseData.Interfaces;
using LinqKit;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AuthenticationBusinessTest
{
    public class BusinessUsersTest
    {
        private IMapper _imapper;
        private IBusinessUsers _iBusinessUsers;

        Mock<IDataUsers> _moqDataUsers;
        Mock<IRepoBase> _moqRepoBase;

        private User _user;

        public BusinessUsersTest()
        {
            _moqDataUsers = new Mock<IDataUsers>();
            _moqRepoBase = new Mock<IRepoBase>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<EntityUser, User>();
                mc.CreateMap<User, EntityUser>();
            });
            _imapper = mappingConfig.CreateMapper();

            _moqRepoBase.Setup(a => a.Exists<EntityUser>(b => b.Username == string.Empty, default)).Returns(Task.FromResult(true));
            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            _user = new User
            {
                Username = "username",
                Password = "password"
            };
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Create_Test(bool usernameExists)
        {
            _moqRepoBase.Setup(a => a.Exists(It.IsAny<Expression<Func<EntityUser, bool>>>(), default)).Returns(Task.FromResult(usernameExists));
            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            var createRequestResult = await _iBusinessUsers.Create(_user, default);

            Assert.Equal(!usernameExists, createRequestResult.Succeeded);
            Assert.Equal(string.Empty, createRequestResult.Model?.Password ?? string.Empty);
        }
    }
}
