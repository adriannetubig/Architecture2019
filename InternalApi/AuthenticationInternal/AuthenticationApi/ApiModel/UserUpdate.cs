using AuthenticationBusiness.Models;
using System.Collections.Generic;

namespace AuthenticationApi.ApiModel
{
    public class UserUpdate
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }
    }
}
