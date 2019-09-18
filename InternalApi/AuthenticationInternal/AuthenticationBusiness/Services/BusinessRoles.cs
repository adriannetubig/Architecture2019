using AuthenticationBusiness.Interfaces;
using AuthenticationBusiness.Models;
using AuthenticationData.Entities;
using AutoMapper;
using BaseData.Interfaces;
using BaseModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationBusiness.Services
{
    public class BusinessRoles: IBusinessRoles
    {
        private readonly IMapper _iMapper;
        private readonly IRepoBase _iRepoBase;

        public BusinessRoles(IMapper iMapper, IRepoBase iRepoBase)
        {
            _iMapper = iMapper;
            _iRepoBase = iRepoBase;
        }

        public async Task<RequestResult<List<Role>>> Read(CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<List<Role>>();

            var entityRoles = await _iRepoBase.ReadMultiple<EntityRole>(a => true, cancellationToken);

            requestResult.Model = _iMapper.Map<List<Role>>(entityRoles);

            return requestResult;
        }

    }
}
