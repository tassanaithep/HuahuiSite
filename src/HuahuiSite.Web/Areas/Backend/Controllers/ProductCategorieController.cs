using System;
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
    public class ProductCategorieController : Controller
    {
        #region Members

        private readonly ILoginService _loginService;

        private readonly IProductCategorieService _productCategorieService;

        #endregion

        #region Constructor

        public ProductCategorieController
        (
            ILoginService loginService,
            IProductCategorieService productCategorieService
        )
        {
            _loginService = loginService;
            _productCategorieService = productCategorieService;
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
            if (!_loginService.CheckLoginStatus())
            {
                return RedirectToAction("Index", "Login");
            }

            ProductCategorieViewModel productCategorieViewModel = new ProductCategorieViewModel();

            try
            {
                _productCategorieService.GetProductCategorieList(ref productCategorieViewModel);
            }
            catch (Exception exception)
            {

            }

            return View(productCategorieViewModel);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Save.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public IActionResult Save(ProductCategorieViewModel productCategorieViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productCategorieService.SaveProductCategorie(productCategorieViewModel);
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
        public IActionResult Update(ProductCategorieViewModel productCategorieViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productCategorieService.UpdateProductCategorie(productCategorieViewModel);
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
        public JsonResult Delete(ProductCategorieViewModel productCategorieViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productCategorieService.DeleteProductCategorie(productCategorieViewModel);
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
            ProductCategorieViewModel productCategorieViewModel = new ProductCategorieViewModel();

            try
            {
                _productCategorieService.GetProductCategorieList(ref productCategorieViewModel);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", productCategorieViewModel);
        }

        #endregion
    }
}