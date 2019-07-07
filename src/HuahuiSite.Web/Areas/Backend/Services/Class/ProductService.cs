using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using System;
using System.Collections.Generic;
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

        //public void SaveProduct(ProductViewModel productViewModel)
        //{
        //    Product Product = new Product()
        //    {
        //        Id = 0,
        //        TitleNameEN = productViewModel.TitleNameEN,
        //        TitleNameTH = productViewModel.TitleNameTH,
        //        DescriptionEN = productViewModel.DescriptionEN,
        //        DescriptionTH = productViewModel.DescriptionTH,
        //        Active = false,
        //        CreatedDate = DateTime.Now,
        //    };

        //    _unitOfWork.Products.Add(Product);
        //}

        /// <summary>
        /// Update Product.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateProduct(Product Product)
        {
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
        }

        #endregion
    }
}
