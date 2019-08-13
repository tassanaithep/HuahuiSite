﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Frontend.Controllers
{
    [Area("Frontend")]
    public class CartController : Controller
    {

        #region Members

        private readonly ICartService _cartService;

        #endregion

        #region Constructor

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        #endregion

        #region Views

        public IActionResult Index()
        {
            MainViewModel mainViewModel = new MainViewModel();

            try
            {
                _cartService.GetCartItemList(ref mainViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(mainViewModel);
        }

        #endregion

        #region Actions

        [HttpPost]
        public JsonResult AddToCart(CartViewModel cartViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _cartService.SaveCart(cartViewModel);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        [HttpPost]
        public JsonResult UpdateCart([FromBody]IEnumerable<CartItemListModel> cartItemList)
        {
            CartViewModel cartViewModel = new CartViewModel();
            cartViewModel.CartItemModelList = cartItemList;

            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _cartService.UpdateCart(cartViewModel);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        [HttpGet]
        public JsonResult CheckOut(int cartId)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _cartService.CheckOut(cartId);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        #endregion
    }
}