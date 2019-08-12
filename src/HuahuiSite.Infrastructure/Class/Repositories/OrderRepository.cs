﻿using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }

        public IEnumerable<OrderModel> GetCompleteOrderList()
        {
            return (from order in HuahuiDbContext.Order.Where(w => w.Status.Equals("Approve"))
                    join customerJoin in HuahuiDbContext.Customer on order.UserId equals customerJoin.Id into OrderJoinCustomer
                    from customer in OrderJoinCustomer.DefaultIfEmpty()
                    join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
                    from sale in CustomerJoinSale.DefaultIfEmpty()
                    select new OrderModel
                    {
                        Id = order.Id,
                        CustomerName = customer.Firstname + " " + customer.Lastname,
                        SaleName = sale.Firstname + " " + sale.Lastname,
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }
    }
}
