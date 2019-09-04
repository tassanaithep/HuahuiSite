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
    public class CustomerController : Controller
    {
        #region Members

        private readonly ILoginService _loginService;

        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly ISaleService _saleService;

        #endregion

        #region Constructor

        public CustomerController
        (
            ILoginService loginService,
            ICustomerService customerService,
            IUserService userService,
            ISaleService saleService
        )
        {
            _loginService = loginService;
            _customerService = customerService;
            _userService = userService;
            _saleService = saleService;
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

            CustomerViewModel customerViewModel = new CustomerViewModel();

            try
            {
                _customerService.GetCustomerList(ref customerViewModel);
                _saleService.GetSaleSelectList(ref customerViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(customerViewModel);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Save.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public IActionResult Save(CustomerViewModel customerViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                int customerId = _customerService.SaveCustomer(customerViewModel);
                _userService.SaveUser(null, null, customerViewModel, customerId);
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
        /// Update.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public IActionResult Update(CustomerViewModel customerViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _customerService.UpdateCustomer(customerViewModel);
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
        /// Delete.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public JsonResult Delete(CustomerViewModel customerViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _customerService.DeleteCustomer(customerViewModel);
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
            CustomerViewModel customerViewModel = new CustomerViewModel();

            try
            {
                _customerService.GetCustomerList(ref customerViewModel);
                _saleService.GetSaleSelectList(ref customerViewModel);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", customerViewModel);
        }

        #endregion
    }
}