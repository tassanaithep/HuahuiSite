using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using HuahuiSite.Core.Models;
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

        public IEnumerable<CartModel> GetCartList()
        {
            return (from cart in HuahuiDbContext.Cart
                    join userJoin in HuahuiDbContext.User on cart.UserId equals userJoin.RoleId into CartJoinUser
                    from user in CartJoinUser.DefaultIfEmpty()
                    select new CartModel
                    {
                        Id = cart.Id,
                        OrderId = cart.OrderId,
                        UserRole = cart.UserRole,
                        UserId = cart.UserId,
                        Status = cart.Status,
                        IsActive = cart.IsActive,
                        CreatedDateTime = cart.CreatedDateTime,
                        UpdatedDateTime = cart.UpdatedDateTime,
                        UserName = user.Name
                    });
        }

        public Cart GetCartActiveByUser(int userId)
        {
            return HuahuiDbContext.Cart.FirstOrDefault(w => w.UserId.Equals(userId) && w.IsActive.Equals(true));
        }
    }
}
