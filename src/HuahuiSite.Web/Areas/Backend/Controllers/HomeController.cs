using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Backend.Controllers
{
    [Area("Backend")]
    public class HomeController : Controller
    {

        #region Members

        private readonly ILoginService _loginService;

        #endregion

        #region Constructor

        public HomeController
        (
            ILoginService loginService
        )
        {
            _loginService = loginService;
        }

        #endregion

        /// <summary>
        /// Index of Home Page.
        /// </summary>
        /// // Author: Mod Nattasit
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public IActionResult Index()
        {
            // Check Login Status
            if (!_loginService.CheckLoginStatus())
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}