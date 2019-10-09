using AutoMapper;
using BaseData.Interfaces;
using BaseModel;
using ExamBusiness.Interfaces;
using ExamBusiness.Models;
using ExamData.Entities;
using ExamData.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExamBusiness.Services
{
    public class BusinessFibonaccis : IBusinessFibonaccis
    {
        private readonly IMapper _iMapper;
        private readonly IDataFibonaccis _iDataFibonaccis;
        private readonly IRepoBase _iRepoBase;

        public BusinessFibonaccis(IMapper iMapper, IRepoBase iRepoBase, IDataFibonaccis iDataFibonaccis)
        {
            _iMapper = iMapper;
            _iRepoBase = iRepoBase;
            _iDataFibonaccis = iDataFibonaccis;
        }

        public async Task<RequestResult<Fibonacci>> Create(Fibonacci fibonacci, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<Fibonacci>();
            var entityFibonacci = _iMapper.Map<EntityFibonacci>(fibonacci);

            entityFibonacci.Total = FibonacciTotal(fibonacci.Iterations);

            await _iRepoBase.Create(entityFibonacci, cancellationToken);
            requestResult.Model = _iMapper.Map<Fibonacci>(entityFibonacci);

            return requestResult;
        }

        public async Task<RequestResult<Fibonacci>> Read(int fibonacciId, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<Fibonacci>();

            var entityFibonacci = await _iRepoBase.ReadSingle<EntityFibonacci>(a => a.FibonacciId == fibonacciId, cancellationToken);
            requestResult.Model = _iMapper.Map<Fibonacci>(entityFibonacci);

            return requestResult;
        }

        public async Task<RequestResult<PagedList<Fibonacci>>> Read(PageFilter pageFilter, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult<PagedList<Fibonacci>>();

            if (pageFilter.ItemsPerPage > 100)
            {
                pageFilter.ItemsPerPage = 100;
                requestResult.Errors.Add("Cannot return more than 100 records at a time");
            }

            var queryResult = await _iDataFibonaccis.Read(pageFilter.Offset, pageFilter.ItemsPerPage);

            var entityFibonaccis = queryResult.Item1;

            var pagedFibonaccis = new PagedList<Fibonacci>
            {
                Items = _iMapper.Map<List<Fibonacci>>(entityFibonaccis),
                ItemsPerPage = pageFilter.ItemsPerPage,
                NumberOfItems = queryResult.Item2,
                PageNo = pageFilter.PageNo
            };

            requestResult.Model = pagedFibonaccis;

            return requestResult;
        }

        public async Task<RequestResult> Update(Fibonacci fibonacci, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult();
            var entityFibonacci = await _iRepoBase.ReadSingle<EntityFibonacci>(a => a.FibonacciId == fibonacci.FibonacciId, cancellationToken);

            entityFibonacci.Iterations = fibonacci.Iterations;
            entityFibonacci.Total = FibonacciTotal(fibonacci.Iterations);

            await _iRepoBase.Update(entityFibonacci, cancellationToken);
            return requestResult;
        }

        public async Task<RequestResult> Delete(int fibonacciId, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult();

            await _iRepoBase.Delete<EntityFibonacci>(a => a.FibonacciId == fibonacciId, cancellationToken);

            return requestResult;
        }

        private int FibonacciTotal(int iterations)
        {            
            var previous = 0;
            var next = 1;

            if (iterations < 0)
                next = 0;
            else
                iterations -= 1;

            var iteration = 0;
            while (iteration < iterations)
            {
                var newNext = previous + next;
                previous = next;
                next = newNext;
                iteration++;
            }

            return next;
        }
    }
}
