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
using System.Web.Helpers;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class HomeService : IHomeService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public HomeService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        #endregion

        #region Read

        /// <summary>
        /// Get Sales List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetCompleteOrderList(ref HomeViewModel homeViewModel)
        {
            homeViewModel.OrderList = _unitOfWork.Orders.GetOrderList();
            homeViewModel.OrderItemList = _unitOfWork.OrderItemLists.GetOrderItemList();
            homeViewModel.CompleteOrderItemList = _unitOfWork.OrderItemLists.GetCompleteOrderItemList();
        }

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion

        #endregion

        #region Actions

        public void Search(ref HomeViewModel homeViewModel)
        {
            homeViewModel.OrderList = _unitOfWork.Orders.GetOrderListOfSearch(homeViewModel.StartDate, homeViewModel.EndDate, homeViewModel.CustomerName, homeViewModel.SaleName);

            if (homeViewModel.OrderList.Count() > 0)
            {
                homeViewModel.OrderItemList = _unitOfWork.OrderItemLists.GetOrderItemList();
                homeViewModel.CompleteOrderItemList = _unitOfWork.OrderItemLists.GetCompleteOrderItemList();
            }
        }

        #endregion
    }
}
