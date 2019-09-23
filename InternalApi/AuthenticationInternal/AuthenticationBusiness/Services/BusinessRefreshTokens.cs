using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using AuthenticationData.Entities;
using AutoMapper;
using BaseData.Interfaces;
using BaseModel;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationBusiness.Services
{
    public class BusinessRefreshTokens: IBusinessRefreshTokens
    {
        private readonly IMapper _iMapper;
        private readonly IRepoBase _iRepoBase;

        public BusinessRefreshTokens(IMapper iMapper, IRepoBase iRepoBase)
        {
            _iMapper = iMapper;
            _iRepoBase = iRepoBase;
        }

        public async Task<RequestResult<RefreshToken>> Create(int userId, CancellationToken cancellationToken)
        {
            var functionResult = new RequestResult<RefreshToken>();

            var entityRefreshToken = new EntityRefreshToken
            {
                UserId = userId,

                Token = CreateToken(),
            };

            await _iRepoBase.Create(entityRefreshToken, cancellationToken);

            functionResult.Model = _iMapper.Map<RefreshToken>(entityRefreshToken);

            return functionResult;
        }

        private string CreateToken()
        {
            var token = string.Empty;
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            return token;
        }

        public async Task<RequestResult<RefreshToken>> Refresh(int userId, string refreshToken, CancellationToken cancellationToken)
        {
            var functionResult = new RequestResult<RefreshToken>();

            var entityRefreshToken = await _iRepoBase.ReadSingle<EntityRefreshToken>(a => a.UserId == userId && a.Token == refreshToken, cancellationToken);
            if (entityRefreshToken != null)
                return await Update(cancellationToken, userId, entityRefreshToken);
            else
                functionResult.Errors.Add("Invalid Refresh Token");

            return functionResult;
        }

        private async Task<RequestResult<RefreshToken>> Update(CancellationToken cancellationToken, int userId, EntityRefreshToken entityRefreshToken)
        {
            var functionResult = new RequestResult<RefreshToken>();

            entityRefreshToken.Token = CreateToken();
            await _iRepoBase.Update(entityRefreshToken, cancellationToken);

            functionResult.Model = _iMapper.Map<RefreshToken>(entityRefreshToken);

            return functionResult;
        }

    }
}
