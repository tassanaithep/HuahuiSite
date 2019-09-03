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
        IEnumerable<CartModel> GetCartListData();
        IEnumerable<CartModel> GetCartListOfSearch(string startDate, string endDate, string customerName, string saleName);
        Cart GetCartActiveByUser(int userId);
        Cart GetCartByOrder(string orderId);
        IEnumerable<Cart> GetCartByLikeOrderId(string orderId);
    }
}
