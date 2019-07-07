using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string username, string password);
    }
}
