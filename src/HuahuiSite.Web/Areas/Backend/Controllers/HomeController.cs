﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Backend.Controllers
{
    [Area("Backend")]
    public class HomeController : Controller
    {

        #region Members

        private readonly ILoginService _loginService;
        private readonly IHomeService _homeService;

        #endregion

        #region Constructor

        public HomeController
        (
            ILoginService loginService,
            IHomeService homeService
        )
        {
            _loginService = loginService;
            _homeService = homeService;
        }

        #endregion

        #region Views

        /// <summary>
        /// Index of Home Page.
        /// </summary>
        /// // Author: Mod Nattasit
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public IActionResult Index()
        {
            #region Check Login

            if (!_loginService.CheckLoginStatus())
            {
                return Redirect("/Backend/Login/Index");
            }

            #endregion

            HomeViewModel homeViewModel = new HomeViewModel();

            try
            {
                _homeService.GetCompleteOrderList(ref homeViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(homeViewModel);
        }

        #endregion

        #region Partial Views

        /// <summary>
        /// Update Table.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public PartialViewResult UpdateTable()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            try
            {
                _homeService.GetCompleteOrderList(ref homeViewModel);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", homeViewModel);
        }

        #endregion

        #region Actions

        [HttpPost]
        public IActionResult Search(HomeViewModel homeViewModel)
        {
            try
            {
                _homeService.Search(ref homeViewModel);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return View("Index", homeViewModel);
        }

        [HttpGet]
        public IActionResult ExportToFile(string orderId)
        {
            byte[] result = new byte[0];

            try
            {
                _homeService.ExportToFile(ref result, orderId);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return File(result, "application/ms-excel", $"Report_"+ orderId + ".xlsx");
        }

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