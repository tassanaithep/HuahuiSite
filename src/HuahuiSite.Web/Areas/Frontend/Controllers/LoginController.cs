using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HuahuiSite.Web.Areas.Frontend.Controllers
{
    [Area("Frontend")]
    public class LoginController : Controller
    {

        #region Members

        private readonly ILoginService _loginService;

        #endregion

        #region Constructor

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        #endregion

        #region Views

        /// <summary>
        /// Index of Page.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public IActionResult Index(string logout)
        {
            if (logout == "true")
            {
                _loginService.Logout();
            }

            return View();
        }

        #endregion

        #region Actions

        [HttpPost]
        public JsonResult CheckLogin(LoginViewModel loginViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _loginService.CheckLogin(loginViewModel);
                HttpContext.Session.SetString("username", loginViewModel.Username);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        #endregion
    }
}