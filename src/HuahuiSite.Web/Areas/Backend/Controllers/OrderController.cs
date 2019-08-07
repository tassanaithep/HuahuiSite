//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using HuahuiSite.Core.Entities;
//using HuahuiSite.Web.Areas.Backend.Models;
//using HuahuiSite.Web.Areas.Backend.Services.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace HuahuiSite.Web.Areas.Backend.Controllers
//{
//    [Area("Backend")]
//    public class OrderController : Controller
//    {
//        #region Members

//        private readonly IOrderService _customerService;

//        #endregion

//        #region Constructor

//        public OrderController(
//            IOrderService customerService
//            )
//        {
//            _customerService = customerService;
//        }

//        #endregion

//        #region Views

//        /// <summary>
//        /// Index of Page.
//        /// </summary>
//        // Author: Mod Nattasit
//        // Updated: 07/07/2019
//        public IActionResult Index()
//        {
//            // Check Login Status
//            //if (!_loginService.CheckLoginStatus())
//            //{
//            //    return RedirectToAction("Index", "Login");
//            //}

//            OrderViewModel customerViewModel = new OrderViewModel();

//            try
//            {
//                _customerService.GetOrderList(ref customerViewModel);
//            }
//            catch (Exception exception)
//            {

//            }

//            return View(customerViewModel);
//        }

//        #endregion

//        //#region Actions

//        ///// <summary>
//        ///// Save.
//        ///// </summary>
//        //// Author: Mod Nattasit
//        //// Updated: 07/07/2019
//        //[HttpPost]
//        //public IActionResult Save(OrderViewModel customerViewModel)
//        //{
//        //    bool isSuccess;
//        //    string exceptionMessage = string.Empty;

//        //    try
//        //    {
//        //        int customerId = _customerService.SaveOrder(customerViewModel);
//        //        _userService.SaveUser(null, null, customerViewModel, customerId);
//        //        isSuccess = true;
//        //    }
//        //    catch (Exception exception)
//        //    {
//        //        exceptionMessage = exception.Message;
//        //        isSuccess = false;
//        //    }

//        //    return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
//        //}

//        ///// <summary>
//        ///// Update.
//        ///// </summary>
//        //// Author: Mod Nattasit
//        //// Updated: 07/07/2019
//        //[HttpPost]
//        //public IActionResult Update(OrderViewModel customerViewModel)
//        //{
//        //    bool isSuccess;
//        //    string exceptionMessage = string.Empty;

//        //    try
//        //    {
//        //        _customerService.UpdateOrder(customerViewModel);
//        //        //_userService.UpdateUser(null, null, customerViewModel);
//        //        isSuccess = true;
//        //    }
//        //    catch (Exception exception)
//        //    {
//        //        exceptionMessage = exception.Message;
//        //        isSuccess = false;
//        //    }

//        //    return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
//        //}

//        ///// <summary>
//        ///// Delete.
//        ///// </summary>
//        //// Author: Mod Nattasit
//        //// Updated: 07/07/2019
//        //[HttpPost]
//        //public JsonResult Delete(OrderViewModel customerViewModel)
//        //{
//        //    bool isSuccess;
//        //    string exceptionMessage = string.Empty;

//        //    try
//        //    {
//        //        _customerService.DeleteOrder(customerViewModel);
//        //        isSuccess = true;
//        //    }
//        //    catch (Exception exception)
//        //    {
//        //        exceptionMessage = exception.Message;
//        //        isSuccess = false;
//        //    }

//        //    return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
//        //}

//        ///// <summary>
//        ///// Update Table.
//        ///// </summary>
//        //// Author: Mod Nattasit
//        //// Updated: 07/07/2019
//        //public PartialViewResult UpdateTable()
//        //{
//        //    OrderViewModel customerViewModel = new OrderViewModel();

//        //    try
//        //    {
//        //        _customerService.GetOrderList(ref customerViewModel);
//        //    }
//        //    catch (Exception exception)
//        //    {

//        //    }

//        //    return PartialView("_Table", customerViewModel);
//        //}

//        //#endregion
//    }
//}