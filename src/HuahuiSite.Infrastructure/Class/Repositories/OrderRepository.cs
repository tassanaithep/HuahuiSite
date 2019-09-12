using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using HuahuiSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public Order GetOrderByOrderId(string orderId)
        {
            return HuahuiDbContext.Order.Where(w => w.Id.Equals(orderId)).SingleOrDefault();
        }

        public IOrderedQueryable<Order> GetOrderListData(string keywordForSearch)
        {
            return HuahuiDbContext.Order.AsNoTracking().Where(w => w.Id.Contains(keywordForSearch) || w.Status.Contains(keywordForSearch)).OrderBy(o => o.Id);
        }

        //tassanai 
        //GetOrderListSearch
        public IEnumerable<OrderModel> GetOrderListSearch(string keywordForSearch)
        {
            return (from order in HuahuiDbContext.Order
                    join customerJoin in HuahuiDbContext.Customer on order.UserId equals customerJoin.Id into OrderJoinCustomer
                    from customer in OrderJoinCustomer
                    join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
                    from sale in CustomerJoinSale
                    select new OrderModel
                    {
                        Id = order.Id,
                        CustomerName = customer.Firstname + " " + customer.Lastname,
                        SaleName = sale.Firstname + " " + sale.Lastname,
                        Status = order.Status,
                        CreatedDateTime = order.CreatedDateTime
                    }).Where(x=>x.Id.Contains(keywordForSearch)||x.SaleName.Contains(keywordForSearch) || x.CustomerName.Contains(keywordForSearch)).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<OrderModel> GetOrderList()
        {
            return (from order in HuahuiDbContext.Order
                    join customerJoin in HuahuiDbContext.Customer on order.UserId equals customerJoin.Id into OrderJoinCustomer
                    from customer in OrderJoinCustomer
                    join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
                    from sale in CustomerJoinSale
                    select new OrderModel
                    {
                        Id = order.Id,
                        CustomerName = customer.Firstname + " " + customer.Lastname,
                        SaleName = sale.Firstname + " " + sale.Lastname,
                        Status = order.Status,
                        CreatedDateTime = order.CreatedDateTime
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<OrderModel> GetOrderListOfSearch(string startDate, string endDate, string customerName, string saleName)
        {

            //var dataresult = (from order in HuahuiDbContext.Order
            //                  join customerJoin in HuahuiDbContext.Customer on order.UserId equals customerJoin.Id into OrderJoinCustomer
            //                  from customer in OrderJoinCustomer
            //                  join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
            //                  from sale in CustomerJoinSale
            //                  select new OrderModel
            //                  {
            //                      Id = order.Id,
            //                      CustomerName = customer.Firstname + " " + customer.Lastname,
            //                      SaleName = sale.Firstname + " " + sale.Lastname,
            //                      Status = order.Status,
            //                      CreatedDateTime = order.CreatedDateTime
            //                  }).Where(w => w.CreatedDateTime.Date >= Convert.ToDateTime(startDate).Date && w.CreatedDateTime.Date <= Convert.ToDateTime(endDate).Date || w.CustomerName.Contains(customerName) || w.SaleName.Contains(saleName)).GroupBy(g => g.Id).Select(s => s.First()).ToList();

            DateTime datestart = DateTime.ParseExact(startDate, "MM/dd/yyyy",
                                    CultureInfo.InvariantCulture);

            DateTime dateend = DateTime.ParseExact(endDate, "MM/dd/yyyy",
                                 CultureInfo.InvariantCulture);
            var dataresult = (from order in HuahuiDbContext.Order
                              join customerJoin in HuahuiDbContext.Customer on order.UserId equals customerJoin.Id into OrderJoinCustomer
                              from customer in OrderJoinCustomer
                              join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
                              from sale in CustomerJoinSale
                              select new OrderModel
                              {
                                  Id = order.Id,
                                  CustomerName = customer.Firstname + " " + customer.Lastname,
                                  SaleName = sale.Firstname + " " + sale.Lastname,
                                  Status = order.Status,
                                  CreatedDateTime = order.CreatedDateTime
                              }).Where(w => w.CreatedDateTime.Date >= datestart && w.CreatedDateTime.Date <= dateend && w.CustomerName.Contains(customerName) || w.SaleName.Contains(saleName)).GroupBy(g => g.Id).Select(s => s.First()).ToList();

            return dataresult;
        }
        //    return (from order in HuahuiDbContext.Order
        //            join customerJoin in HuahuiDbContext.Customer on order.UserId equals customerJoin.Id into OrderJoinCustomer
        //            from customer in OrderJoinCustomer
        //            join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
        //            from sale in CustomerJoinSale
        //            select new OrderModel
        //            {
        //                Id = order.Id,
        //                CustomerName = customer.Firstname + " " + customer.Lastname,
        //                SaleName = sale.Firstname + " " + sale.Lastname,
        //                Status = order.Status,
        //                CreatedDateTime = order.CreatedDateTime
        //            }).Where(w => w.CreatedDateTime.Date >= Convert.ToDateTime(startDate).Date && w.CreatedDateTime.Date <= Convert.ToDateTime(endDate).Date || w.CustomerName.Contains(customerName) || w.SaleName.Contains(saleName)).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        //}

        public Order GetOrderActiveByUser(int userId)
        {
            return HuahuiDbContext.Order.FirstOrDefault(w => w.UserId.Equals(userId) && w.IsActive.Equals(true));
        }

        public IEnumerable<Order> GetOrderByLikeOrderId(string orderId)
        {
            return HuahuiDbContext.Order.Where(w => w.Id.Contains(orderId)).ToList();
        }
    }
}
