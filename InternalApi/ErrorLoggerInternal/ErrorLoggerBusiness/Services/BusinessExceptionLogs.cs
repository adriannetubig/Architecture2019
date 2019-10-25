using AutoMapper;
using BaseData.Interfaces;
using BaseModel;
using ErrorLoggerBusiness.Interfaces;
using ErrorLoggerBusiness.Models;
using ErrorLoggerData.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErrorLoggerBusiness.Services
{
    public class BusinessExceptionLogs : IBusinessExceptionLogs
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

            requestResult.Model = _iMapper.Map<ExceptionLog>(entityExceptionLog);

            return requestResult;
        }

        public async Task<RequestResult<PagedList<ExceptionLog>>> Read(CancellationToken cancellationToken, bool ascending, int itemsPerPage, int pageNo, string sortBy)
        {
            var requestResult = new RequestResult<PagedList<ExceptionLog>>();

            var entityPagedList = await _iRepoBase.ReadMultiple<EntityExceptionLog>(a => true, ascending, itemsPerPage, pageNo, sortBy, cancellationToken, a => a.InnerExceptionLog);

            requestResult.Model = _iMapper.Map<PagedList<ExceptionLog>>(entityPagedList);

            return requestResult;
        }
    }
}
