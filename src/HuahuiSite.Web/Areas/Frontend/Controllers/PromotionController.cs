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
    public class PromotionController : Controller
    {

        #region Members

        private readonly IPromotionService _promotionService;

        #endregion

        #region Constructor

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        #endregion

        #region Views

        public IActionResult Index()
        {
            MainViewModel mainViewModel = new MainViewModel();

            try
            {
                _promotionService.GetPromotionList(ref mainViewModel);
            }
            catch (Exception exception)
            {

                throw;
            }
            return View(mainViewModel);
        }

        #endregion
    }
}