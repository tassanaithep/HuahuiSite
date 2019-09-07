using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class ProductService : IProductService
    {
        #region Members

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public ProductService
        (
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        /// <summary>
        /// Save Product.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void SaveProduct(ProductViewModel productViewModel)
        {
            #region Save Image to Directory

            string pictureImageFileName = string.Empty;

            if (productViewModel.PictureFile != null)
            {
                pictureImageFileName = Guid.NewGuid().ToString() + Path.GetExtension(productViewModel.PictureFile.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", pictureImageFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productViewModel.PictureFile.CopyTo(fileStream);
                }
            }

            #endregion

            #region Create Object to Save

            Product product = new Product()
            {
                Code = productViewModel.Code,
                Name = productViewModel.Name,
                UnitId = productViewModel.UnitId,
                ProductCategorieCode = productViewModel.ProductCategorieCode,
                ProductGroupCode = productViewModel.ProductGroupCode,
                IsLicense = productViewModel.IsLicense,
                IsPromotion = productViewModel.IsPromotion,
                IsActive =true,
                PictureFileName = productViewModel.PictureFile != null ? pictureImageFileName : null,
                CreatedDateTime = DateTime.Now
            };

            #endregion

            _unitOfWork.Products.Add(product);
        }

        #endregion

        #region Read

        /// <summary>
        /// Get Product List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetProductList(ref ProductViewModel productViewModel, string keywordForSearch)
        {
            #region Check Keyword for Search from Session

            if (keywordForSearch == string.Empty && (_httpContextAccessor.HttpContext.Session.GetString("KeywordForSearch") != null && _httpContextAccessor.HttpContext.Session.GetString("KeywordForSearch") != string.Empty))
            {
                keywordForSearch = _httpContextAccessor.HttpContext.Session.GetString("KeywordForSearch");
            }

            #endregion

            #region Get List

            productViewModel.ProductList = _unitOfWork.Products.GetProductListData(keywordForSearch);

            #endregion

            _httpContextAccessor.HttpContext.Session.SetString("KeywordForSearch", keywordForSearch);

            #region Get Select List

            productViewModel.UnitOfProductSelectList = _unitOfWork.UnitOfProducts.GetAll().Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
            productViewModel.ProductCategorieSelectList = _unitOfWork.ProductCategories.GetAll().Select(s => new SelectListItem { Value = s.Code, Text = s.Name });
            productViewModel.ProductGroupSelectList = _unitOfWork.ProductGroups.GetAll().Select(s => new SelectListItem { Value = s.Code, Text = s.Name });

            #endregion
        }

        #endregion

        #region Update

        /// <summary>
        /// Update Product.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateProduct(ProductViewModel productViewModel)
        {
            #region Update Image

            string pictureFileName = string.Empty;

            #region Delete Old Image

            if ((productViewModel.PictureFile != null || productViewModel.IsRemoveImage) && productViewModel.PictureFileName != null)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", productViewModel.PictureFileName);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            #endregion

            #region Save New Image

            if (productViewModel.PictureFile != null && !productViewModel.IsRemoveImage)
            {
                pictureFileName = Guid.NewGuid().ToString() + Path.GetExtension(productViewModel.PictureFile.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", pictureFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productViewModel.PictureFile.CopyTo(fileStream);
                }
            }

            #endregion

            #endregion

            #region Create Object to Update

            Product product = new Product()
            {
                Id = productViewModel.Id,
                Code = productViewModel.Code,
                Name = productViewModel.Name,
                UnitId = productViewModel.UnitId,
                ProductCategorieCode = productViewModel.ProductCategorieCode,
                ProductGroupCode = productViewModel.ProductGroupCode,
                IsLicense = productViewModel.IsLicense,
                IsPromotion = productViewModel.IsPromotion,
                IsActive = productViewModel.IsActive,
                PictureFileName = productViewModel.PictureFile == null && !productViewModel.IsRemoveImage ? productViewModel.PictureFileName : productViewModel.PictureFile == null && productViewModel.IsRemoveImage ? null : pictureFileName,
                CreatedDateTime = productViewModel.CreatedDateTime,
                UpdatedDateTime = DateTime.Now
            };

            #endregion

            _unitOfWork.Products.Update(product);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete Product.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void DeleteProduct(ProductViewModel productViewModel)
        {
            #region Create Object to Delete

            Product product = new Product()
            {
                Id = productViewModel.Id,
            };

            #endregion

            _unitOfWork.Products.Remove(product);

            #region Delete Image

            //string imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", productViewModel.TitlePictureFileName);

            //if (System.IO.File.Exists(imagePath))
            //{
            //    System.IO.File.Delete(imagePath);
            //}

            #endregion
        }

        #endregion

        #endregion
    }
}
