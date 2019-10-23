using AutoMapper;
using BaseData.Interfaces;
using BaseModel;
using ErrorLoggerBusiness.Models;
using ErrorLoggerData.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorLoggerBusiness.Services
{
    public class BusinessExceptionLogs
    {
        private readonly IMapper _iMapper;
        private readonly IRepoBase _iRepoBase;

        public BusinessExceptionLogs(IMapper iMapper, IRepoBase iRepoBase)
        {
            _iMapper = iMapper;
            _iRepoBase = iRepoBase;
        }

        public async Task<RequestResult<ExceptionLog>> Create(CancellationToken cancellationToken, ExceptionLog exceptionLog, string applicationName)
        {
            var requestResult = new RequestResult<ExceptionLog>();

            var entityExceptionLog = _iMapper.Map<EntityExceptionLog>(exceptionLog);
            entityExceptionLog.ApplicationName = applicationName;
            entityExceptionLog.CreatedDateUtc = DateTime.UtcNow;

            if (entityExceptionLog.InnerExceptionLog != null)
                entityExceptionLog.InnerExceptionLog.CreatedDateUtc = DateTime.UtcNow;

            await _iRepoBase.Create(entityExceptionLog, cancellationToken);

            return requestResult;
        }
    }
}
