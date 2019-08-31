﻿using System;
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
            return (from cart in HuahuiDbContext.Cart.Where(w => w.Status.Equals("Cart") || w.Status.Equals("Confirm"))
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

        public IEnumerable<CartModel> GetConfirmCartList()
        {
            return (from cart in HuahuiDbContext.Cart.Where(w => w.Status.Equals("Confirm"))
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
                        Status = cart.Status
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public Cart GetCartActiveByUser(int userId)
        {
            return HuahuiDbContext.Cart.FirstOrDefault(w => w.UserId.Equals(userId) && w.Status.Equals("Cart") || w.Status.Equals("Confirm") && w.IsActive.Equals(true));
        }

        public Cart GetCartByOrder(string orderId)
        {
            return HuahuiDbContext.Cart.First(w => w.OrderId.Equals(orderId));
        }
    }
}
