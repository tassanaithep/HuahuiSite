﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Backend.Controllers
{
    [Area("Backend")]
    public class SaleController : Controller
    {
        #region Members

        private readonly ISaleService _saleService;
        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public SaleController(
            ISaleService saleService,
            IUserService userService
            )
        {
            _saleService = saleService;
            _userService = userService;
        }

        #endregion

        #region Views

        /// <summary>
        /// Index of Page.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public IActionResult Index()
        {
            // Check Login Status
            //if (!_loginService.CheckLoginStatus())
            //{
            //    return RedirectToAction("Index", "Login");
            //}

            SaleViewModel saleViewModel = new SaleViewModel();

            try
            {
                _saleService.GetSaleList(ref saleViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(saleViewModel);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Save.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public IActionResult Save(SaleViewModel saleViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _saleService.SaveSale(saleViewModel);
                _userService.SaveUser(null, saleViewModel, null);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        /// <summary>
        /// Update.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public IActionResult Update(SaleViewModel saleViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _saleService.UpdateSale(saleViewModel);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        /// <summary>
        /// Delete.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public JsonResult Delete(SaleViewModel saleViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _saleService.DeleteSale(saleViewModel);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        /// <summary>
        /// Update Table.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public PartialViewResult UpdateTable()
        {
            SaleViewModel saleViewModel = new SaleViewModel();

            try
            {
                _saleService.GetSaleList(ref saleViewModel);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", saleViewModel);
        }

        #endregion
    }
}