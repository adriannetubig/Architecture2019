using AuthenticationData.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationData.Interfaces
{
    public interface IDataUsers
    {
        Task<Tuple<List<EntityUser>, int>> Read(int offset, int fetch);
    }
}
