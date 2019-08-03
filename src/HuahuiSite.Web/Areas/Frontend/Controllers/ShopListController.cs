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
    public class ShopListController : Controller
    {

        #region Members

        private readonly IShopListService _shopListService;

        #endregion
        #region Constructor
        public ShopListController(IShopListService shopListService)
        {
            _shopListService = shopListService;
        }
        #endregion
        public IActionResult Index(string param)
        {

            ShopListViewModel shopListViewModel = new ShopListViewModel();
           


            try
            {
               _shopListService.GetShopList(ref shopListViewModel);
                if(param != null)
                {
                    shopListViewModel.ProductList = shopListViewModel.ProductList.Where(x => x.ProductCategorieCode == param).ToList();

                }
                //else
                //{
                //    _shopListService.GetShopList(ref shopListViewModel);

                //}


            }
            catch (Exception exception)
            {

                throw;
            }
            return View(shopListViewModel);
        }
    }
}