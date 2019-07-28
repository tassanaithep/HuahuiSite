using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCartActiveByUser(int userId);
    }
}
