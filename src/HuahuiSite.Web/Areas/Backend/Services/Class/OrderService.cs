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
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class OrderService : IOrderService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        #endregion

        #region Read

        /// <summary>
        /// Get Order List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetOrderList(ref OrderViewModel orderViewModel)
        {
            orderViewModel.OrderList = _unitOfWork.Orders.GetAll();
            orderViewModel.OrderItemList = _unitOfWork.OrderItemLists.GetOrderItemList();
        }

        #endregion

        #region Update



        #endregion

        #region Delete

        #endregion

        #endregion

        #region Actions

        public void CompleteOrder(int orderId)
        {
            #region Update Cart Status

            var cart = _unitOfWork.Carts.GetCartByOrder(orderId);

            cart.Status = "Complete";
            cart.IsActive = false;

            _unitOfWork.Carts.Update(cart);

            #endregion

            #region Update Order Status

            var order = _unitOfWork.Orders.Get(orderId);

            order.Status = "Complete";
            order.IsActive = false;

            _unitOfWork.Orders.Update(order);

            #endregion
        }

        #endregion
    }
}
