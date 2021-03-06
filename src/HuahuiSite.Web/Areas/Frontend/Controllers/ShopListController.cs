﻿using System;
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

        public IActionResult Index(string param, string param2, string textsearch)
        {
            MainViewModel mainViewModel = new MainViewModel();

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
                    
                    if(param !="all" && textsearch !=null)
                    {
                        mainViewModel.ShopListViewModel.ProductList = mainViewModel.ShopListViewModel.ProductList.Where(x => x.ProductCategorieCode == param && x.Name.Contains(textsearch)).ToList();
                    }
                    else if (param == "all" && textsearch != null)
                    {
                        mainViewModel.ShopListViewModel.ProductList = mainViewModel.ShopListViewModel.ProductList.Where(x => x.Name.Contains(textsearch)).ToList();

                    }
                    else
                    {
                        mainViewModel.ShopListViewModel.ProductList = mainViewModel.ShopListViewModel.ProductList.Where(x => x.ProductCategorieCode == param ).ToList();

                    }

                    mainViewModel.ProductGroupList = mainViewModel.ProductGroupList.Where(x => x.ProductCategorieCode == param).ToList();

                    mainViewModel.ShopListViewModel.Param = param;
                }
                else if(param != null && param2 != null)
                {
                    mainViewModel.ShopListViewModel.ProductList = mainViewModel.ShopListViewModel.ProductList.Where(x => x.ProductCategorieCode == param && x.ProductGroupCode==param2).ToList();
                    mainViewModel.ProductGroupList = mainViewModel.ProductGroupList.Where(x => x.ProductCategorieCode == param).ToList();
                    mainViewModel.ShopListViewModel.Param = param;
                    mainViewModel.ShopListViewModel.Param2 = param2;
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