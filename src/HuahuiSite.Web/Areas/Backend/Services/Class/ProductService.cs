using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
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

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public ProductService(IUnitOfWork unitOfWork)
        {
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

            string titlePictureImageFileName = string.Empty;

            if (productViewModel.TitlePictureImageFile != null)
            {
                titlePictureImageFileName = Guid.NewGuid().ToString() + Path.GetExtension(productViewModel.TitlePictureImageFile.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", titlePictureImageFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productViewModel.TitlePictureImageFile.CopyTo(fileStream);
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
                IsActive = productViewModel.IsActive,
                PictureFileName = productViewModel.PictureFileName,
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
        public void GetProductList(ref ProductViewModel productViewModel)
        {
            productViewModel.ProductList = _unitOfWork.Products.GetAll();
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

            string titlePictureImageFileName = string.Empty;

            #region Delete Old Image

            //if ((productViewModel.TitlePictureImageFile != null || productViewModel.IsRemoveImage) && productViewModel.TitlePictureFileName != null)
            //{
            //    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", productViewModel.TitlePictureFileName);

            //    if (System.IO.File.Exists(imagePath))
            //    {
            //        System.IO.File.Delete(imagePath);
            //    }
            //}

            #endregion

            #region Save New Image

            if (!productViewModel.IsRemoveImage)
            {
                titlePictureImageFileName = Guid.NewGuid().ToString() + Path.GetExtension(productViewModel.TitlePictureImageFile.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", titlePictureImageFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productViewModel.TitlePictureImageFile.CopyTo(fileStream);
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
                PictureFileName = productViewModel.PictureFileName,
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
