using AutoMapper;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class CartService : ICartService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        #endregion

        #region Read

        /// <summary>
        /// Get Cart List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetCartList(ref CartViewModel cartViewModel)
        {
            cartViewModel.CartList = _unitOfWork.Carts.GetCartList();
            cartViewModel.CartItemListModelList = _unitOfWork.CartItemLists.GetCartItemList();
        }

        #endregion

        #region Update

        /// <summary>
        /// Update Cart.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateCart(CartViewModel cartViewModel)
        {
            #region Delete Old Cart Item List

            var cartItemListToRemove = _unitOfWork.CartItemLists.GetCartItemListByCardId(cartViewModel.CartItemList.First().CardId);
            _unitOfWork.CartItemLists.RemoveRange(cartItemListToRemove);

            #endregion

            #region Update Cart Item List

            cartViewModel.CartItemList.ToList().ForEach(i => i.Id = 0);

            _unitOfWork.CartItemLists.AddRange(cartViewModel.CartItemList);

            #endregion
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete Cart.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void DeleteCart(int cartId)
        {
            // Delete Cart
            var cart = _unitOfWork.Carts.Get(cartId);
            _unitOfWork.Carts.Remove(cart);

            // Delete Cart Item
            var cartItemList = _unitOfWork.CartItemLists.GetCartItemListByCardId(cartId);
            _unitOfWork.CartItemLists.RemoveRange(cartItemList);
        }

        #endregion

        #endregion

        #region Actions

        public void ApproveCart(int cartId)
        {
            #region Get Cart

            var cart = _unitOfWork.Carts.Get(cartId);
            var cartItemList = _unitOfWork.CartItemLists.GetCartItemListByCardId(cartId);

            #endregion

            #region Save Order

            #region Create Object to Save

            Order order = new Order()
            {
                Id = cart.OrderId,
                UserRole = cart.UserRole,
                UserId = cart.UserId,
                CustomerId = cart.CustomerId,
                Status = "Approve",
                IsActive = true,
                CreatedDateTime = DateTime.Now
            };

            #endregion

            _unitOfWork.Orders.Add(order);

            #endregion

            #region Save Order Item List

            List<OrderItemList> orderItemList = new List<OrderItemList>();

            cartItemList.ToList().ForEach(i =>
            {
                orderItemList.Add(new OrderItemList
                {
                    OrderId = cart.OrderId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice,
                    CreatedDateTime = DateTime.Now
                });
            });

            _unitOfWork.OrderItemLists.AddRange(orderItemList);

            #endregion

            #region Remove Cart

            _unitOfWork.Carts.Remove(cart);
            _unitOfWork.CartItemLists.RemoveRange(cartItemList);

            #endregion
        }

        #endregion
    }
}
