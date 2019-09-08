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
    public class ProductGroupController : Controller
    {
        #region Members

        private readonly ILoginService _loginService;

        private readonly IProductGroupService _productGroupService;

        #endregion

        #region Constructor

        public ProductGroupController
        (
            ILoginService loginService,
            IProductGroupService productGroupService
        )
        {
            _loginService = loginService;
            _productGroupService = productGroupService;
        }

        #endregion

        #region Views

        /// <summary>
        /// Index of Page.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpGet]
        public IActionResult Index(int page = 1, string keywordForSearch = "", bool isUpdate = false)
        //public async Task<IActionResult> Index(int page = 1, string keywordForSearch = "", bool isUpdate = false)
        {
            #region Check Login

            if (!_loginService.CheckLoginStatus())
            {
                return Redirect("/Backend/Login/Index");
            }

            #endregion

                ProductGroupViewModel productGroupViewModel = new ProductGroupViewModel();

            try
            {
                _productGroupService.GetProductGroupList(ref productGroupViewModel, keywordForSearch, isUpdate, page);

                // Bind Model to Paging Model
                productGroupViewModel.ProductGroupPagingList =  PagingList.Create(productGroupViewModel.ProductGroupList, 10, page);
              //  ViewBag.keywordForSearch = productGroupViewModel.keywordForSearch;



            }

            catch (Exception exception)
            {

            }

            return View(productGroupViewModel);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Save.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        [HttpPost]
        public IActionResult Save(ProductGroupViewModel productGroupViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productGroupService.SaveProductGroup(productGroupViewModel);
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
        public IActionResult Update(ProductGroupViewModel productGroupViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productGroupService.UpdateProductGroup(productGroupViewModel);
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
        public JsonResult Delete(ProductGroupViewModel productGroupViewModel)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productGroupService.DeleteProductGroup(productGroupViewModel);
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
            ProductGroupViewModel productGroupViewModel = new ProductGroupViewModel();

            try
            {
                _productGroupService.GetProductGroupList(ref productGroupViewModel, string.Empty, false, 1);
            }
            catch (Exception exception)
            {

            }

            return PartialView("_Table", productGroupViewModel);
        }

        #endregion
    }
}