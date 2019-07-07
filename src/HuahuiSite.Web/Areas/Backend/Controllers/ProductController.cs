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
    public class ProductController : Controller
    {
        #region Members

        private readonly IProductService _productService;

        #endregion

        #region Constructor

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        /// <summary>
        /// Index of Product Page.
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

            ProductViewModel productViewModel = new ProductViewModel();

            try
            {
                _productService.GetProductList(ref productViewModel);
            }
            catch (Exception ex)
            {

            }

            return View(productViewModel);
        }

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
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productService.UpdateProduct(product);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        [HttpPost]
        public JsonResult Delete(Product product)
        {
            bool isSuccess;
            string exceptionMessage = string.Empty;

            try
            {
                _productService.DeleteProduct(product);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
                isSuccess = false;
            }

            return Json(new { isSuccess = isSuccess, exceptionMessage = exceptionMessage });
        }

        public PartialViewResult UpdateTable()
        {
            ProductViewModel productViewModel = new ProductViewModel();

            try
            {
                _productService.GetProductList(ref productViewModel);
            }
            catch (Exception ex)
            {

            }

            return PartialView("_Table", productViewModel);
        }
    }
}