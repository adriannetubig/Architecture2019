﻿using AutoMapper;
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

            if (fibonacci.Iterations > 46)
            {
                requestResult.Errors.Add("Cannot compute for more than 46");
                entityFibonacci.Total = 0;
            }
            else
            {
                entityFibonacci.Total = FibonacciTotal(fibonacci.Iterations);
            }

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

        public async Task<RequestResult<PagedList<Fibonacci>>> Read(int pageNo, int itemsPerPage)
        {
            var requestResult = new RequestResult<PagedList<Fibonacci>>();

            var offset = 0;

            if (itemsPerPage > 100)
            {
                itemsPerPage = 100;
                requestResult.Errors.Add("Cannot return more than 100 records at a time");
                offset = (pageNo - 1) * itemsPerPage;
            }

            var queryResult = await _iDataFibonaccis.Read(offset, itemsPerPage);

            var entityFibonaccis = queryResult.Item1;

            var pagedFibonaccis = new PagedList<Fibonacci>
            {
                Items = _iMapper.Map<List<Fibonacci>>(entityFibonaccis),
                ItemsPerPage = itemsPerPage,
                NumberOfItems = queryResult.Item2,
                PageNo = pageNo
            };

            requestResult.Model = pagedFibonaccis;

            return requestResult;
        }

        public async Task<RequestResult> Update(Fibonacci fibonacci, CancellationToken cancellationToken)
        {
            var requestResult = new RequestResult();
            var entityFibonacci = await _iRepoBase.ReadSingle<EntityFibonacci>(a => a.FibonacciId == fibonacci.FibonacciId, cancellationToken);

            entityFibonacci.Iterations = fibonacci.Iterations;

            if (fibonacci.Iterations > 46)
            {
                requestResult.Errors.Add("Cannot compute for more than 46");
                entityFibonacci.Total = 0;
            }
            else
            {
                entityFibonacci.Total = FibonacciTotal(fibonacci.Iterations);
            }

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

            if (iterations <= 0)
            {
                next = 0;
            }
            else
            {
                iterations -= 1;
            }

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
