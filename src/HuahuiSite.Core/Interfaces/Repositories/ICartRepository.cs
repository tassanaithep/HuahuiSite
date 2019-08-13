using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        IEnumerable<CartModel> GetCartList();
        Cart GetCartActiveByUser(int userId);
        Cart GetCartByOrder(int orderId);
    }
}
