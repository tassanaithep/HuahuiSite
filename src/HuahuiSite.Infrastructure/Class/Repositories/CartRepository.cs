using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }

        public Cart GetCartActiveByUser(int userId)
        {
            return HuahuiDbContext.Cart.FirstOrDefault(w => w.UserId.Equals(userId) && w.IsActive.Equals(true));
        }
    }
}
