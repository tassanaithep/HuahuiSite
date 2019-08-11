using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Backend.Controllers
{
    [Area("Backend")]
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

        /// <summary>
        /// Index of Page.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public IActionResult Index()
        {
            // Check Login Status
            if (!_loginService.CheckLoginStatus())
            {
                return RedirectToAction("Index", "Login");
            }

            CartViewModel cartViewModel = new CartViewModel();

            try
            {
                _cartService.GetCartList(ref cartViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(cartViewModel);
        }

        #endregion

        #region Actions

        [HttpPost]
        public JsonResult Update([FromBody]IEnumerable<CartItemList> cartItemList)
        {
            CartViewModel cartViewModel = new CartViewModel();
            cartViewModel.CartItemList = cartItemList;

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
        public JsonResult Delete(int cartId)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _cartService.DeleteCart(cartId);
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
        public JsonResult Approve(int cartId)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _cartService.ApproveCart(cartId);
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

        //#region Actions

        ///// <summary>
        ///// Save.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //[HttpPost]
        //public IActionResult Save(CustomerViewModel customerViewModel)
        //{
        //    bool isSuccess;
        //    string exceptionMessage = string.Empty;

        //    try
        //    {
        //        int customerId = _customerService.SaveCustomer(customerViewModel);
        //        _userService.SaveUser(null, null, customerViewModel, customerId);
        //        isSuccess = true;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionMessage = exception.Message;
        //        isSuccess = false;
        //    }

        //    return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        //}

        ///// <summary>
        ///// Update.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //[HttpPost]
        //public IActionResult Update(CustomerViewModel customerViewModel)
        //{
        //    bool isSuccess;
        //    string exceptionMessage = string.Empty;

        //    try
        //    {
        //        _customerService.UpdateCustomer(customerViewModel);
        //        //_userService.UpdateUser(null, null, customerViewModel);
        //        isSuccess = true;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionMessage = exception.Message;
        //        isSuccess = false;
        //    }

        //    return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        //}

        ///// <summary>
        ///// Delete.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //[HttpPost]
        //public JsonResult Delete(CustomerViewModel customerViewModel)
        //{
        //    bool isSuccess;
        //    string exceptionMessage = string.Empty;

        //    try
        //    {
        //        _customerService.DeleteCustomer(customerViewModel);
        //        isSuccess = true;
        //    }
        //    catch (Exception exception)
        //    {
        //        exceptionMessage = exception.Message;
        //        isSuccess = false;
        //    }

        //    return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        //}

        /// <summary>
        /// Update Table.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public PartialViewResult UpdateTable()
        {
            CartViewModel cartViewModel = new CartViewModel();

            try
            {
                _cartService.GetCartList(ref cartViewModel);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", cartViewModel);
        }

        //#endregion
    }
}