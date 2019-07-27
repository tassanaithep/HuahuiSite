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
    public class ProductCategorieService : IProductCategorieService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public ProductCategorieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        /// <summary>
        /// Save Product Categorie.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void SaveProductCategorie(ProductCategorieViewModel productCategorieViewModel)
        {
            #region Create Object to Save

            ProductCategorie productCategorie = new ProductCategorie()
            {
                Code = productCategorieViewModel.Code,
                Name = productCategorieViewModel.Name,
                IsActive = true,
                CreatedDateTime = DateTime.Now
            };

            #endregion

            _unitOfWork.ProductCategories.Add(productCategorie);
        }

        #endregion

        #region Read

        /// <summary>
        /// Get Product Categorie List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetProductCategorieList(ref ProductCategorieViewModel productCategorieViewModel)
        {
            productCategorieViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
        }

        #endregion

        #region Update

        /// <summary>
        /// Update Product Categorie.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateProductCategorie(ProductCategorieViewModel productCategorieViewModel)
        {
            #region Create Object to Update

            ProductCategorie productCategorie = new ProductCategorie()
            {
                Id = productCategorieViewModel.Id,
                Code = productCategorieViewModel.Code,
                Name = productCategorieViewModel.Name,
                CreatedDateTime = productCategorieViewModel.CreatedDateTime,
                UpdatedDateTime = DateTime.Now
            };

            #endregion

            _unitOfWork.ProductCategories.Update(productCategorie);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete Product Categorie.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void DeleteProductCategorie(ProductCategorieViewModel productCategorieViewModel)
        {
            #region Create Object to Delete

            ProductCategorie productCategorie = new ProductCategorie()
            {
                Id = productCategorieViewModel.Id,
            };

            #endregion

            _unitOfWork.ProductCategories.Remove(productCategorie);
        }

        #endregion

        #endregion
    }
}
