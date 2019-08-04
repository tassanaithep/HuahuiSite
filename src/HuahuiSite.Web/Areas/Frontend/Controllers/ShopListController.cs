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
        public IActionResult Index(string param,string param2)
        {
            MainViewModel mainViewModel = new MainViewModel();
         //   ShopListViewModel shopListViewModel = new ShopListViewModel();

            try
            {
               _shopListService.GetShopList(ref mainViewModel);

                //set default param = first
                if(param== null)
                {
                    param = mainViewModel.ProductCategorieList.FirstOrDefault().Code;
                }
               
                // หา productgroup name 

                if (param != null && param2==null)
                {
                    

                    mainViewModel.ShopListViewModel.ProductList = mainViewModel.ShopListViewModel.ProductList.Where(x => x.ProductCategorieCode == param).ToList();
                    mainViewModel.ProductGroupList = mainViewModel.ProductGroupList.Where(x => x.ProductCategorieCode == param).ToList();

                    mainViewModel.ShopListViewModel.Param = param;
                }
                else if(param != null && param2 != null)
                {
                    //param2 = mainViewModel.ProductGroupList.Where(m=>m.ProductCategorieCode==param2).FirstOrDefault().Code;
                    //  mainViewModel.ProductGroupList =
                    mainViewModel.ShopListViewModel.ProductList = mainViewModel.ShopListViewModel.ProductList.Where(x => x.ProductCategorieCode == param && x.ProductGroupCode==param2).ToList();
                    mainViewModel.ProductGroupList = mainViewModel.ProductGroupList.Where(x => x.ProductCategorieCode == param).ToList();
                    //  mainViewModel.ProductGroupList.ToList().Clear();

                    // _shopListService.GetShopList(ref shopListViewModel);

                }


            }
            catch (Exception exception)
            {

                throw;
            }
            return View(mainViewModel);
        }
    }
}