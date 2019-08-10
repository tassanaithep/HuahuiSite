using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }

        public IEnumerable<User> GetAdminUserList()
        {
            return HuahuiDbContext.User.Where(w => w.RoleName.Equals("Admin")).ToList();
        }

        public User GetUserOfLogin(string username, string password)
        {
            return HuahuiDbContext.User.First(w => w.Username.Equals(username) && w.Password.Equals(password) );
        }

        public User GetUserByRole(int roleId)
        {
            return HuahuiDbContext.User.First(w => w.RoleId.Equals(roleId));
        }
    }
}
