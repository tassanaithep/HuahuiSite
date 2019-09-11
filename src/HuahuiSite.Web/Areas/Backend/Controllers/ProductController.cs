using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace HuahuiSite.Web.Areas.Backend.Controllers
{
    [Area("Backend")]
    public class ProductController : Controller
    {
        #region Members

        private readonly ILoginService _loginService;
        private readonly IProductService _productService;

        #endregion

        #region Constructor

        public ProductController
        (
            ILoginService loginService,
            IProductService productService
        )
        {
            _loginService = loginService;
            _productService = productService;
        }

        #endregion

        #region Views

        /// <summary>
        /// Index of Page.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string keywordForSearch = "", bool isUpdate = false)
        {
            #region Check Login

            if (!_loginService.CheckLoginStatus())
            {
                return Redirect("/Backend/Login/Index");
            }

            #endregion

            ProductViewModel productViewModel = new ProductViewModel();

            try
            {
                

                _productService.GetProductList(ref productViewModel, keywordForSearch, isUpdate, page);

                // Bind Model to Paging Model
                productViewModel.ProductPagingList = await PagingList.CreateAsync(productViewModel.ProductList, 10, page);
            }
            catch (Exception exception)
            {

            }

            return View(productViewModel);
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
            ProductViewModel productViewModel = new ProductViewModel();

            try
            {
                _productService.GetProductList(ref productViewModel, string.Empty, false, 1);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", productViewModel);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Save.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public IActionResult Save(ProductViewModel productViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productService.SaveProduct(productViewModel);
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
        public IActionResult Update(ProductViewModel productViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productService.UpdateProduct(productViewModel);
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
        public JsonResult Delete(ProductViewModel productViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productService.DeleteProduct(productViewModel);
                isSuccess = true;
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        #endregion
    }
}