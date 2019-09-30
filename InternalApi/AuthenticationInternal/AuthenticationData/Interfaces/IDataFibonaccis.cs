using AuthenticationData.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationData.Interfaces
{
    public interface IDataFibonaccis
    {
        Task<Tuple<List<EntityFibonacci>, int>> Read(int offset, int fetch);
    }
}
