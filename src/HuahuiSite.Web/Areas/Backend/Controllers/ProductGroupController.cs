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
    public class ProductGroupController : Controller
    {
        #region Members

        private readonly ILoginService _loginService;

        private readonly IProductGroupService _productCategorieService;

        #endregion

        #region Constructor

        public ProductGroupController
        (
            ILoginService loginService,
            IProductGroupService productCategorieService
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
            #region Check Login

            if (!_loginService.CheckLoginStatus())
            {
                return Redirect("/Backend/Login/Index");
            }

            #endregion

            ProductGroupViewModel productCategorieViewModel = new ProductGroupViewModel();

            try
            {
                _productCategorieService.GetProductGroupList(ref productCategorieViewModel);
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
        public IActionResult Save(ProductGroupViewModel productCategorieViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productCategorieService.SaveProductGroup(productCategorieViewModel);
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
        public IActionResult Update(ProductGroupViewModel productCategorieViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productCategorieService.UpdateProductGroup(productCategorieViewModel);
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
        public JsonResult Delete(ProductGroupViewModel productCategorieViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productCategorieService.DeleteProductGroup(productCategorieViewModel);
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
            ProductGroupViewModel productCategorieViewModel = new ProductGroupViewModel();

            try
            {
                _productCategorieService.GetProductGroupList(ref productCategorieViewModel);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", productCategorieViewModel);
        }

        #endregion
    }
}