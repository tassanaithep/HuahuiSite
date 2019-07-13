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
            string titlePictureImageFileName = string.Empty;

            #region Save Image

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

            Product Product = new Product()
            {
                //TitleNameEN = productViewModel.TitleNameEN,
                TitleNameTh = productViewModel.TitleNameTh,
                //DescriptionEN = productViewModel.DescriptionEN,
                DescriptionTh = productViewModel.DescriptionTh,
                TitlePictureFileName = productViewModel.TitlePictureImageFile != null ? titlePictureImageFileName : null,
                IsActive = true,
                CreatedDate = DateTime.Now,
            };

            #endregion

            _unitOfWork.Products.Add(Product);
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
            string titlePictureImageFileName = string.Empty;

            #region Update Image

            #region Delete Old Image

            if ((productViewModel.TitlePictureImageFile != null || productViewModel.IsRemoveImage) && productViewModel.TitlePictureFileName != null)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", productViewModel.TitlePictureFileName);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

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

            Product Product = new Product()
            {
                Id = productViewModel.Id,
                //TitleNameEN = productViewModel.TitleNameEN,
                TitleNameTh = productViewModel.TitleNameTh,
                //DescriptionEN = productViewModel.DescriptionEN,
                DescriptionTh = productViewModel.DescriptionTh,
                TitlePictureFileName = productViewModel.TitlePictureImageFile == null && !productViewModel.IsRemoveImage ? productViewModel.TitlePictureFileName : productViewModel.TitlePictureImageFile == null && productViewModel.IsRemoveImage ? null : titlePictureImageFileName,
                //IsActive = true,
                CreatedDate = productViewModel.CreatedDate,
                UpdatedDate = DateTime.Now
            };

            #endregion

            _unitOfWork.Products.Update(Product);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete Product.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void DeleteProduct(Product product)
        {
            _unitOfWork.Products.Remove(product);

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", product.TitlePictureFileName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        #endregion

        #endregion
    }
}
