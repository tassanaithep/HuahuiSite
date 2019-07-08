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

        /// <summary>
        /// Get Product List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetProductList(ref ProductViewModel productViewModel)
        {
            productViewModel.ProductList = _unitOfWork.Products.GetAll();
        }

        public void SaveProduct(ProductViewModel productViewModel)
        {
            string TitlePictureImageFileName = Guid.NewGuid().ToString() + Path.GetExtension(productViewModel.TitlePictureImageFile.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", TitlePictureImageFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                productViewModel.TitlePictureImageFile.CopyTo(fileStream);
            }

            Product Product = new Product()
            {
                //TitleNameEN = productViewModel.TitleNameEN,
                TitleNameTh = productViewModel.TitleNameTh,
                //DescriptionEN = productViewModel.DescriptionEN,
                DescriptionTh = productViewModel.DescriptionTh,
                TitlePictureFileName = TitlePictureImageFileName,
                IsActive = true,
                CreatedDate = DateTime.Now,
            };

            _unitOfWork.Products.Add(Product);
        }

        /// <summary>
        /// Update Product.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateProduct(ProductViewModel productViewModel)
        {
            // Delete Old Image
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", productViewModel.TitlePictureFileName);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            // New Image
            string TitlePictureImageFileName = Guid.NewGuid().ToString() + Path.GetExtension(productViewModel.TitlePictureImageFile.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images\upload", TitlePictureImageFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                productViewModel.TitlePictureImageFile.CopyTo(fileStream);
            }

            Product Product = new Product()
            {
                Id = productViewModel.Id,
                //TitleNameEN = productViewModel.TitleNameEN,
                TitleNameTh = productViewModel.TitleNameTh,
                //DescriptionEN = productViewModel.DescriptionEN,
                DescriptionTh = productViewModel.DescriptionTh,
                TitlePictureFileName = TitlePictureImageFileName,
                IsActive = true,
                CreatedDate = DateTime.Now,
            };

            _unitOfWork.Products.Update(Product);
        }

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
    }
}
