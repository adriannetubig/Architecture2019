using BaseData.Services;
using Dapper;
using ExamData.Entities;
using ExamData.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamData.Services
{
    public class DataFibonaccis : SqlBase, IDataFibonaccis
    {
        public DataFibonaccis(string connectionString) : base(connectionString)
        {
        }

        public async Task<Tuple<List<EntityFibonacci>, int>> Read(int offset, int fetch)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Offset", offset);
            parameter.Add("@Fetch", fetch);
            return await PagedQuery<EntityFibonacci>(parameter, "GetFibonaccisPaged");
        }
    }
}
