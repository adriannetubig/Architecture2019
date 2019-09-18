using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using AuthenticationData.Entities;
using AuthenticationData.Interfaces;
using AutoMapper;
using BaseData.Interfaces;
using BaseModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationBusiness.Services
{
    public class BusinessUsers: IBusinessUsers
    {
        private readonly IMapper _iMapper;
        private readonly IDataUsers _iDataUsers;
        private readonly IRepoBase _iRepoBase;

        public BusinessUsers(IMapper iMapper, IRepoBase iRepoBase, IDataUsers iDataUsers)
        {
            _iMapper = iMapper;
            _iRepoBase = iRepoBase;
            _iDataUsers = iDataUsers;
        }

        public async Task<RequestResult<User>> Create(User user, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<User>();

            var usernameExists = await _iRepoBase.Exists<EntityUser>(a => a.Username == user.Username, cancellationToken);

            if (!usernameExists)
            {
                var entityUser = _iMapper.Map<EntityUser>(user);
                entityUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                await _iRepoBase.Create(entityUser, cancellationToken);

                entityUser.Password = string.Empty;
                requestResult.Model = _iMapper.Map<User>(entityUser);
            }
            else
            {
                requestResult.Errors.Add("Username already exists");
            }

            return requestResult;
        }

        public async Task<RequestResult<User>> Read(int userId, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<User>();

            var entityUser = await _iRepoBase.ReadSingle<EntityUser>(a => a.UserId == userId, cancellationToken);
            requestResult.Model = _iMapper.Map<User>(entityUser);

            return requestResult;
        }

        public async Task<RequestResult<PagedList<User>>> Read(PageFilter pageFilter, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<PagedList<User>>();

            if (pageFilter.ItemsPerPage > 100)
            {
                pageFilter.ItemsPerPage = 100;
                requestResult.Errors.Add("Cannot return more than 100 records at a time");
            }

            var queryResult = await _iDataUsers.Read(pageFilter.Offset, pageFilter.ItemsPerPage);

            var entityUsers = queryResult.Item1;
            var users = queryResult.Item1;

            var pagedUsers = new PagedList<User>
            {
                Items = _iMapper.Map<List<User>>(entityUsers),
                ItemsPerPage = pageFilter.ItemsPerPage,
                NumberOfItems = queryResult.Item2,
                PageNo = pageFilter.PageNo
            };

            requestResult.Model = pagedUsers;

            return requestResult;
        }

        public async Task<RequestResult<User>> Authenticate(User user, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<User>();

            var entityUser = await _iRepoBase.ReadSingle<EntityUser>(a => a.Username == user.Username, cancellationToken);

            if (entityUser != null && BCrypt.Net.BCrypt.Verify(user.Password, entityUser.Password))
                requestResult.Model = _iMapper.Map<User>(entityUser);
            else
                requestResult.Errors.Add("Incorrect Username or Password");

            return requestResult;
        }

        public async Task<RequestResult<User>> Validate(User user, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<User>();

            var isValid = await _iRepoBase.Exists<EntityUser>(a => a.Username == user.Username && a.UserId == user.UserId, cancellationToken);

            if (!isValid)
                requestResult.Errors.Add("Invalid Credential");

            return requestResult;
        }

        public async Task<RequestResult> ChangePassword(User user, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult();
            var entityUser = await _iRepoBase.ReadSingle<EntityUser>(a => a.Username == user.Username, cancellationToken, a => a.Role);

            if (BCrypt.Net.BCrypt.Verify(user.Password, entityUser.Password))
            {
                entityUser.Password = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);

                await _iRepoBase.Update(entityUser, cancellationToken);
            }
            else
            {
                requestResult.Errors.Add("Incorrect Username or Password");
            }

            return requestResult;
        }

        public async Task<RequestResult> Update(User user, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult();
            var entityUser = await _iRepoBase.ReadSingle<EntityUser>(a => a.UserId == user.UserId, cancellationToken);

            entityUser.RoleId = user.RoleId;

            await _iRepoBase.Update(entityUser, cancellationToken);
            return requestResult;
        }

        public async Task<RequestResult> UpdatePassword(User user, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult();

            var entityUser = await _iRepoBase.ReadSingle<EntityUser>(a => a.UserId == user.UserId, cancellationToken);

            entityUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _iRepoBase.Update(entityUser, cancellationToken);

            return requestResult;
        }

        public async Task<RequestResult> Delete(int userId, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult();

            await _iRepoBase.Delete<EntityUser>(a => a.UserId == userId, cancellationToken);

            return requestResult;
        }
    }
}
