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

        public User GetUserOfLogin(string username, string password)
        {
            return HuahuiDbContext.User.First(w => w.Username.Equals(username) && w.Password.Equals(password) && w.RoleName.Equals("Admin"));
        }
    }
}
