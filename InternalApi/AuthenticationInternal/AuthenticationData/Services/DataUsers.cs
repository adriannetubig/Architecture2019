using AuthenticationData.Entities;
using AuthenticationData.Interfaces;
using BaseData.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationData.Services
{
    public class DataUsers : SqlBase, IDataUsers
    {
        public DataUsers(string connectionString): base(connectionString)
        {
        }

        public async Task<Tuple<List<EntityUser>, int>> Read(int offset, int fetch)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Offset", offset);
            parameter.Add("@Fetch", fetch);
            return await PagedQuery<EntityUser>(parameter, "GetUsersPaged");
        }
    }
}
