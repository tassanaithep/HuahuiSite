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

        #region Views

        public IActionResult Index()
        {
            MainViewModel mainViewModel = new MainViewModel();

            try
            {
                _homeService.GetHome(ref mainViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(mainViewModel);
        }

        #endregion

        #region Actions

        [HttpGet]
        public JsonResult GetProductPriceByQuantity(string productGroupCode, int quantity)
        {
            ProductGroupViewModel productGroupViewModel = new ProductGroupViewModel();

            try
            {
                _homeService.GetProductPriceByQuantity(ref productGroupViewModel, productGroupCode, quantity);
            }
            catch (Exception exception)
            {

                throw;
            }

            return Json(productGroupViewModel);
        }

        #endregion
    }
}