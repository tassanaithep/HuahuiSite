using AutoMapper;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Http;
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

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public OrderService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
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
        public void GetOrderList(ref OrderViewModel orderViewModel, string keywordForSearch, bool isUpdate, int page)
        {
            if (!isUpdate)
            {
                #region Check Keyword for Search from Session

                if (keywordForSearch == string.Empty && (_httpContextAccessor.HttpContext.Session.GetString("KeywordForSearch") != null && _httpContextAccessor.HttpContext.Session.GetString("KeywordForSearch") != string.Empty))
                {
                    keywordForSearch = _httpContextAccessor.HttpContext.Session.GetString("KeywordForSearch");
                }

                #endregion
            }
            else
            {
                _httpContextAccessor.HttpContext.Session.Remove("KeywordForSearch");
            }

            #region Get List

            orderViewModel.OrderList = _unitOfWork.Orders.GetAll().AsQueryable().OrderBy(o => o.Id);

            #endregion



            orderViewModel.OrderItemList = _unitOfWork.OrderItemLists.GetOrderItemList();
        }

        #endregion

        #region Update



        #endregion

        #region Delete

        #endregion

        #endregion

        #region Actions

        public void CompleteOrder(string orderId)
        {
            #region Update Order Status

            var order = _unitOfWork.Orders.GetOrderByOrderId(orderId);

            order.Status = "Complete";
            order.IsActive = false;

            _unitOfWork.Orders.Update(order);

            #endregion
        }

        public void CancelOrder(string orderId)
        {
            #region Delete Order Status

            Order order = new Order()
            {
                Id = orderId
            };

            _unitOfWork.Orders.Remove(order);

            #endregion
        }

        #endregion
    }
}
