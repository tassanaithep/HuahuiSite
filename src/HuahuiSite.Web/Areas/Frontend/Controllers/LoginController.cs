using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HuahuiSite.Core.Entities;

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
            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.ProductCategorieList = new List<ProductCategorie>();
            mainViewModel.ProductGroupList = new List<ProductGroup>();
            mainViewModel.LoginViewModel = new LoginViewModel();

            if (logout == "true")
            {
                _loginService.Logout();
            }

            return View(mainViewModel);
        }

        #endregion

        #region Actions

        [HttpPost]
        public JsonResult CheckLogin(MainViewModel mainViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _loginService.CheckLogin(mainViewModel.LoginViewModel);
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