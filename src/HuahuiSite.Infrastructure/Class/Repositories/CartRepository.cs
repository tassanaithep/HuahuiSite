using System;
using System.Collections.Generic;
using System.Globalization;
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
            //return (from cart in HuahuiDbContext.Cart.Where(w => w.Status.Equals("Cart") || w.Status.Equals("Confirm"))
            //        join userJoin in HuahuiDbContext.User on cart.UserId equals userJoin.RoleId into CartJoinUser
            //        from user in CartJoinUser.DefaultIfEmpty()
            //        select new CartModel
            //        {
            //            Id = cart.Id,
            //            OrderId = cart.OrderId,
            //            UserRole = cart.UserRole,
            //            UserId = cart.UserId,
            //            Status = cart.Status,
            //            IsActive = cart.IsActive,
            //            CreatedDateTime = cart.CreatedDateTime,
            //            UpdatedDateTime = cart.UpdatedDateTime,
            //            UserName = user.Name
            //        });


            //return (from c in HuahuiDbContext.Cart.Where(w => w.Status.Equals("Cart") || w.Status.Equals("Confirm"))
            //        from u in HuahuiDbContext.User
            //               where c.UserId == u.RoleId && c.UserRole == u.RoleName
            //               select new CartModel
            //               {
            //                   Id = c.Id,
            //                   OrderId = c.OrderId,
            //                   UserRole = c.UserRole,
            //                   UserId = c.UserId,
            //                   Status = c.Status,
            //                   IsActive = c.IsActive,
            //                   CreatedDateTime = c.CreatedDateTime,
            //                   UpdatedDateTime = c.UpdatedDateTime,
            //                   UserName = u.Name
            //               });

            //return (from c in HuahuiDbContext.Cart.Where(w => w.Status.Equals("Cart") || w.Status.Equals("Confirm"))
            //        from u in HuahuiDbContext.User
            //        where c.UserId == u.RoleId && c.UserRole == u.RoleName
            //        select new CartModel
            //        {
            //            Id = c.Id,
            //            OrderId = c.OrderId,
            //            UserRole = c.UserRole,
            //            UserId = c.UserId,
            //            Status = c.Status,
            //            IsActive = c.IsActive,
            //            CreatedDateTime = c.CreatedDateTime,
            //            UpdatedDateTime = c.UpdatedDateTime,
            //            UserName = u.Name
            //        });

            return (from cart in HuahuiDbContext.Cart.Where(w => w.Status.Equals("Cart") || w.Status.Equals("Confirm"))
                    join customerJoin in HuahuiDbContext.Customer on cart.UserId equals customerJoin.Id into CartJoinCustomer
                    from customer in CartJoinCustomer
                    join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
                    from sale in CustomerJoinSale
                    select new CartModel
                    {
                        Id = cart.Id,
                        OrderId = cart.OrderId,
                        UserRole = cart.UserRole,
                        UserId = cart.UserId,
                        Status = cart.Status,
                        IsActive = cart.IsActive,
                        CustomerName = customer.Firstname + " " + customer.Lastname,
                        SaleName = sale.Firstname + " " + sale.Lastname,            
                        CreatedDateTime = cart.CreatedDateTime,
                        UpdatedDateTime = cart.UpdatedDateTime
                    });


        }

        public IEnumerable<CartModel> GetCartListOfSearch(string startDate, string endDate, string customerName, string saleName)
        {

            //DateTime datestart = DateTime.ParseExact(startDate, "MM/dd/yyyy",
            //                 CultureInfo.InvariantCulture);

            //DateTime datestart = DateTime.ParseExact("dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
            System.Globalization.CultureInfo _cultureEnInfo = new System.Globalization.CultureInfo("en-US");
DateTime datestart = Convert.ToDateTime(startDate, _cultureEnInfo);

            DateTime dateend = DateTime.ParseExact(endDate, "MM/dd/yyyy",
                                 CultureInfo.InvariantCulture);

            return (from cart in HuahuiDbContext.Cart
                    join customerJoin in HuahuiDbContext.Customer on cart.UserId equals customerJoin.Id into CartJoinCustomer
                    from customer in CartJoinCustomer
                    join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
                    from sale in CustomerJoinSale
                    select new CartModel
                    {
                        Id = cart.Id,
                        OrderId = cart.OrderId,
                        CustomerName = customer.Firstname + " " + customer.Lastname,
                        SaleName = sale.Firstname + " " + sale.Lastname,
                        Status = cart.Status,
                        CreatedDateTime = cart.CreatedDateTime
                    }).Where(w => w.CreatedDateTime.Date >= datestart && w.CreatedDateTime.Date <= dateend || w.CustomerName.Contains(customerName) || w.SaleName.Contains(saleName)).GroupBy(g => g.Id).Select(s => s.First()).ToList();


            //return (from cart in HuahuiDbContext.Cart
            //        join customerJoin in HuahuiDbContext.Customer on cart.UserId equals customerJoin.Id into CartJoinCustomer
            //        from customer in CartJoinCustomer
            //        join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
            //        from sale in CustomerJoinSale
            //        select new CartModel
            //        {
            //            Id = cart.Id,
            //            OrderId = cart.OrderId,
            //            CustomerName = customer.Firstname + " " + customer.Lastname,
            //            SaleName = sale.Firstname + " " + sale.Lastname,
            //            Status = cart.Status,
            //            CreatedDateTime = cart.CreatedDateTime
            //        }).Where(w => w.CreatedDateTime.Date >= Convert.ToDateTime(startDate).Date && w.CreatedDateTime.Date <= Convert.ToDateTime(endDate).Date || w.CustomerName.Contains(customerName) || w.SaleName.Contains(saleName)).GroupBy(g => g.Id).Select(s => s.First()).ToList();


        }

        public IEnumerable<CartModel> GetCartListData()
        {
            return (from cart in HuahuiDbContext.Cart
                    join customerJoin in HuahuiDbContext.Customer on cart.UserId equals customerJoin.Id into CartJoinCustomer
                    from customer in CartJoinCustomer
                    join saleJoin in HuahuiDbContext.Sale on customer.SaleId equals saleJoin.Id into CustomerJoinSale
                    from sale in CustomerJoinSale
                    select new CartModel
                    {
                        Id = cart.Id,
                        OrderId = cart.OrderId,
                        CustomerName = customer.Firstname + " " + customer.Lastname,
                        SaleName = sale.Firstname + " " + sale.Lastname,
                        Status = cart.Status,
                        CreatedDateTime = cart.CreatedDateTime
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public Cart GetCartActiveByUser(int userId)
        {
            return HuahuiDbContext.Cart.FirstOrDefault(w => w.UserId.Equals(userId) && w.IsActive.Equals(true));
        }

        public Cart GetCartByOrder(string orderId)
        {
            return HuahuiDbContext.Cart.First(w => w.OrderId.Equals(orderId));
        }

        public IEnumerable<Cart> GetCartByLikeOrderId(string orderId)
        {
            return HuahuiDbContext.Cart.Where(w => w.OrderId.Contains(orderId)).ToList();
        }
    }
}
