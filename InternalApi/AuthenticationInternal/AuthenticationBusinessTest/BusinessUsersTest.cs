using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using AuthenticationBusiness.Services;
using AuthenticationData.Entities;
using AuthenticationData.Interfaces;
using AutoMapper;
using BaseData.Interfaces;
using BaseModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        private readonly EntityUser _entityUser;
        private readonly User _user;
        private PageFilter _pageFilter;

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

            _entityUser = new EntityUser
            {
                Username = "admin",
                Password = "$2b$10$BMH23FkUuDMDtMsLBEbb7u7AJ/eYnV5MbJ.or6hYSnRtfDAuAzTpS"
            };

            _user = new User
            {
                Username = "admin",
                Password = "password",
                NewPassword = "newpassword"
            };

            _pageFilter = new PageFilter
            {
                PageNo = 1,
                ItemsPerPage = 1,
            };
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Create_TestIfExistingUser(bool usernameExists)
        {
            _moqRepoBase.Setup(a => a.Exists(It.IsAny<Expression<Func<EntityUser, bool>>>(), default)).Returns(Task.FromResult(usernameExists));
            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            var requestResult = await _iBusinessUsers.Create(_user, default);

            Assert.Equal(!usernameExists, requestResult.Succeeded);
            Assert.Equal(string.Empty, requestResult.Model?.Password ?? string.Empty);
        }

        [Theory]
        [InlineData(100, true)]
        [InlineData(101, false)]
        public async Task Read_TestPageLimitRestriction(int pages, bool succeeded)
        {
            _moqDataUsers.Setup(a => a.Read(It.IsAny<int>(), It.IsAny<int>())).Returns(
                Task.FromResult(
                new Tuple<List<EntityUser>, int>(new List<EntityUser>(), 1)
                ));

            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            _pageFilter.ItemsPerPage = pages;

            var requestResult = await _iBusinessUsers.Read(_pageFilter, default);

            Assert.Equal(succeeded, requestResult.Succeeded);
            if (succeeded)
                Assert.Equal(pages, requestResult.Model.ItemsPerPage);
            else
                Assert.Equal(100, requestResult.Model.ItemsPerPage);
        }

        [Theory]
        [InlineData("password", true)]
        [InlineData("wrongpassword", false)]
        public async Task Authenticate_TestPassword(string password, bool succeeded)
        {
            _moqRepoBase.Setup(a => a.ReadSingle(It.IsAny<Expression<Func<EntityUser, bool>>>(), default)).Returns(Task.FromResult(_entityUser));

            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            _user.Password = password;

            var requestResult = await _iBusinessUsers.Authenticate(_user, default);

            Assert.Equal(succeeded, requestResult.Succeeded);
            Assert.Equal(string.Empty, requestResult.Model?.Password ?? string.Empty);
        }

        [Fact]
        public async Task Authenticate_TestNotExistingUsername()
        {
            _moqRepoBase.Setup(a => a.ReadSingle(It.IsAny<Expression<Func<EntityUser, bool>>>(), default)).Returns(Task.FromResult<EntityUser>(null));

            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            var requestResult = await _iBusinessUsers.Authenticate(_user, default);

            Assert.False(requestResult.Succeeded);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Validate_TestIfExistingUser(bool validLogin)
        {
            _moqRepoBase.Setup(a => a.Exists(It.IsAny<Expression<Func<EntityUser, bool>>>(), default)).Returns(Task.FromResult(validLogin));
            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            var requestResult = await _iBusinessUsers.Validate(_user, default);

            Assert.Equal(validLogin, requestResult.Succeeded);
        }

        [Theory]
        [InlineData("password", true)]
        [InlineData("wrongpassword", false)]
        public async Task ChangePassword_TestPassword(string password, bool succeeded)
        {
            _moqRepoBase.Setup(a => a.ReadSingle(It.IsAny<Expression<Func<EntityUser, bool>>>(), default)).Returns(Task.FromResult(_entityUser));

            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            _user.Password = password;

            var requestResult = await _iBusinessUsers.ChangePassword(_user, default);

            Assert.Equal(succeeded, requestResult.Succeeded);
        }

        [Fact]
        public async Task ChangePassword_TestNotExistingUsername()
        {
            _moqRepoBase.Setup(a => a.ReadSingle(It.IsAny<Expression<Func<EntityUser, bool>>>(), default)).Returns(Task.FromResult<EntityUser>(null));

            _iBusinessUsers = new BusinessUsers(_imapper, _moqRepoBase.Object, _moqDataUsers.Object);

            var requestResult = await _iBusinessUsers.ChangePassword(_user, default);

            Assert.False(requestResult.Succeeded);
        }
    }
}
