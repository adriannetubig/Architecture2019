using AuthenticationData.Entities;
using AuthenticationData.Interfaces;
using BaseData.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationData.Services
{
    public class DataFibonaccis : SqlBase, IDataFibonaccis
    {
        public DataFibonaccis(string connectionString): base(connectionString)
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
