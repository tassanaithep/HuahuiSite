using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Frontend.Controllers
{
    [Area("Frontend")]
    public class HomeController : Controller
    {

        #region Members

        private readonly IHomeService _homeService;

        #endregion

        #region Constructor

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        #endregion

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            try
            {
                _homeService.GetProductList(ref homeViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(homeViewModel);
        }
    }
}