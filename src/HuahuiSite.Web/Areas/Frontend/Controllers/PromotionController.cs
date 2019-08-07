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
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
        public IActionResult Index()
        {
            MainViewModel mainViewModel = new MainViewModel();
            //   ShopListViewModel shopListViewModel = new ShopListViewModel();
            //if (textsearch==null)
            //{
            //    textsearch = "";
            //}


            try
            {
                _promotionService.GetShopList(ref mainViewModel);

                //set default param = first
                //if (param == null)
                //{
                //    param = mainViewModel.ProductCategorieList.FirstOrDefault().Code;
                //}

                // หา productgroup name 

                //if (param != null && param2 == null)
                //{
                //    if (textsearch != null)
                //    {

                    //   mainViewModel.PromotionViewModel.ProductList = mainViewModel.PromotionViewModel.ProductList
                    //.Where(x => x.IsPromotion == true).OrderBy(x=>x.ProductCategorieCode).ToList();

                mainViewModel.PromotionViewModel.ProductList = mainViewModel.PromotionViewModel.ProductList.Where(x => x.IsPromotion == true).ToList();

                //    }
                //    else
                //    {
                //        mainViewModel.ShopListViewModel.ProductList = mainViewModel.ShopListViewModel.ProductList.Where(x => x.ProductCategorieCode == param).ToList();

                //    }


                //    mainViewModel.ProductGroupList = mainViewModel.ProductGroupList.Where(x => x.ProductCategorieCode == param).ToList();

                //    mainViewModel.ShopListViewModel.Param = param;
                //}
                //else if (param != null && param2 != null)
                //{

                //    //param2 = mainViewModel.ProductGroupList.Where(m=>m.ProductCategorieCode==param2).FirstOrDefault().Code;
                //    //  mainViewModel.ProductGroupList =
                //    mainViewModel.ShopListViewModel.ProductList = mainViewModel.ShopListViewModel.ProductList.Where(x => x.ProductCategorieCode == param && x.ProductGroupCode == param2).ToList();
                //    mainViewModel.ProductGroupList = mainViewModel.ProductGroupList.Where(x => x.ProductCategorieCode == param).ToList();
                //    mainViewModel.ShopListViewModel.Param = param;
                //    mainViewModel.ShopListViewModel.Param2 = param2;

                //    //  mainViewModel.ProductGroupList.ToList().Clear();

                //    // _shopListService.GetShopList(ref shopListViewModel);

                //}


            }
            catch (Exception exception)
            {

                throw;
            }
            return View(mainViewModel);
        }
    }
}