using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface IOrderItemListRepository : IRepository<OrderItemList>
    {
        IEnumerable<OrderItemList> GetOrderItemListByOrder(string orderId);
        IEnumerable<OrderItemListModel> GetOrderItemList();
        IEnumerable<OrderItemListModel> GetCompleteOrderItemList();
        IEnumerable<OrderItemListModel> GetNotCompleteOrderItemList();
        OrderItemListModel GetOrderItemListByCardAndProduct(string orderId, int productId);

    }
}
