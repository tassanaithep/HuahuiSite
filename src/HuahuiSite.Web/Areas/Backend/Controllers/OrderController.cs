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
    public class OrderController : Controller
    {
        #region Members

        private readonly ILoginService _loginService;

        private readonly IOrderService _orderService;

        #endregion

        #region Constructor

        public OrderController
        (
            ILoginService loginService,
            IOrderService orderService
        )
        {
            _loginService = loginService;
            _orderService = orderService;
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
            #region Check Login

            if (!_loginService.CheckLoginStatus())
            {
                return Redirect("/Backend/Login/Index");
            }

            #endregion

            OrderViewModel orderViewModel = new OrderViewModel();

            try
            {
                _orderService.GetOrderList(ref orderViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(orderViewModel);
        }

        #endregion

        #region Actions

        [HttpGet]
        public JsonResult Complete(string orderId)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _orderService.CompleteOrder(orderId);
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
        public JsonResult Cancel(string orderId)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _orderService.CancelOrder(orderId);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        /// <summary>
        /// Update Table.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public PartialViewResult UpdateTable()
        {
            OrderViewModel orderViewModel = new OrderViewModel();

            try
            {
                _orderService.GetOrderList(ref orderViewModel);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", orderViewModel);
        }

        #endregion
    }
}