using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<OrderModel> GetOrderList();
        IEnumerable<OrderModel> GetOrderListOfSearch(string startDate, string endDate, string customerName, string saleName);
    }
}
