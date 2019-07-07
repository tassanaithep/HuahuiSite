using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Backend.Controllers
{
    [Area("Backend")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Index of Home Page.
        /// </summary>
        /// // Author: Mod Nattasit
        // Updated: 07/07/2019
        public IActionResult Index()
        {
            return View();
        }
    }
}