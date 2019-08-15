using HuahuiSite.Core.Entities;
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

        public IEnumerable<OrderModel> GetOrderList()
        {
            return (from order in HuahuiDbContext.Order
                    join customerJoin in HuahuiDbContext.Customer on order.UserId equals customerJoin.Id into OrderJoinCustomer
                    from customer in OrderJoinCustomer.DefaultIfEmpty()
                    join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
                    from sale in CustomerJoinSale.DefaultIfEmpty()
                    select new OrderModel
                    {
                        Id = order.Id,
                        CustomerName = customer.Firstname + " " + customer.Lastname,
                        SaleName = sale.Firstname + " " + sale.Lastname,
                        Status = order.Status
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<OrderModel> GetOrderListOfSearch(string startDate, string endDate, string customerName, string saleName)
        {
            return (from order in HuahuiDbContext.Order
                    join customerJoin in HuahuiDbContext.Customer on order.UserId equals customerJoin.Id into OrderJoinCustomer
                    from customer in OrderJoinCustomer.DefaultIfEmpty()
                    join saleJoin in HuahuiDbContext.Sale on order.UserId equals saleJoin.Id into OrderJoinSale
                    from sale in OrderJoinSale.DefaultIfEmpty()
                    select new OrderModel
                    {
                        Id = order.Id,
                        CustomerName = customer.Firstname + " " + customer.Lastname,
                        SaleName = sale.Firstname + " " + sale.Lastname,
                        Status = order.Status
                    }).Where(w => w.CustomerName.Contains(customerName) || w.SaleName.Contains(saleName)).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }
    }
}
