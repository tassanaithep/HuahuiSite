using System;
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

        private readonly ILoginService _loginService;
        private readonly ICartService _cartService;

        #endregion

        #region Constructor

        public CartController
        (
            ILoginService loginService,
            ICartService cartService
        )
        {
            _loginService = loginService;
            _cartService = cartService;
        }

        #endregion

        #region Views

        public IActionResult Index()
        {
            #region Check Login

            if (!_loginService.CheckLoginStatus())
            {
                return Redirect("/Home/Index");
            }

            #endregion

            MainViewModel mainViewModel = new MainViewModel();

            try
            {
                _cartService.GetCartItemList(ref mainViewModel);

                if (mainViewModel.CartViewModel == null)
                {
                    return RedirectToAction("Index", "Home");
                }
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
        public JsonResult DeleteCartItem(int cartId, int cartItemId)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _cartService.DeleteCartItem(cartId, cartItemId);
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
        public JsonResult CheckOut(int cartId, int customerId)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _cartService.CheckOut(cartId, customerId);
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