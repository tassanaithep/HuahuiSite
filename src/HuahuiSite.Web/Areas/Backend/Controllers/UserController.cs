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
    public class UserController : Controller
    {
        #region Members

        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Views

        /// <summary>
        /// Index of User Page.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public IActionResult Index()
        {
            // Check Login Status
            //if (!_loginService.CheckLoginStatus())
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            UserViewModel userViewModel = new UserViewModel();

            try
            {
                _userService.GetUserList(ref userViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(userViewModel);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Save.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public IActionResult Save(UserViewModel userViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _userService.SaveUser(userViewModel);
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
        public IActionResult Update(UserViewModel userViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _userService.UpdateUser(userViewModel);
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
        public JsonResult Delete(UserViewModel userViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _userService.DeleteUser(userViewModel);
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
            UserViewModel userViewModel = new UserViewModel();

            try
            {
                _userService.GetUserList(ref userViewModel);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", userViewModel);
        }

        #endregion
    }
}