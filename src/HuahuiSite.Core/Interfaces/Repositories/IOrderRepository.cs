﻿using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetOrderByOrderId(string orderId);
        IOrderedQueryable<Order> GetOrderListData(string keywordForSearch);
        IEnumerable<OrderModel> GetOrderList();
        IEnumerable<OrderModel> GetOrderListSearch(string keywordForSearch);
        Order GetOrderActiveByUser(int userId);
        IEnumerable<OrderModel> GetOrderListOfSearch(string startDate, string endDate, string customerName, string saleName);
        IEnumerable<Order> GetOrderByLikeOrderId(string orderId);
    }
}
