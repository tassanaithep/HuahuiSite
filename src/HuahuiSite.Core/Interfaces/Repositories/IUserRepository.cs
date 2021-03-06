﻿using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAdminUserList();
        User GetUserOfLogin(string username, string password);
        User GetUserByRole(string roleName, int roleId);
        User GetUserByRoleId(int roleId);
    }
}
