using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class ProductGroupService : IProductGroupService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public ProductGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        /// <summary>
        /// Save Product Group.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void SaveProductGroup(ProductGroupViewModel productGroupViewModel)
        {
            #region Create Object to Save

            ProductGroup productGroup = new ProductGroup()
            {
                Code = productGroupViewModel.Code,
                Name = productGroupViewModel.Name,
                UnitPrice = productGroupViewModel.UnitPrice,
                PromotionPrice = productGroupViewModel.PromotionPrice,
                MinQuantity = productGroupViewModel.MinQuantity,
                MaxQuantity = productGroupViewModel.MaxQuantity,
                IsActive = true,
                ProductCategorieCode = productGroupViewModel.ProductCategorieCode,
                CreatedDateTime = DateTime.Now
            };

            #endregion

            _unitOfWork.ProductGroups.Add(productGroup);
        }

        #endregion

        #region Read

        /// <summary>
        /// Get Product Group List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetProductGroupList(ref ProductGroupViewModel productGroupViewModel)
        {
            #region Get List

            productGroupViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();

            #endregion

            #region Get Select List

            productGroupViewModel.ProductCategorieSelectList = _unitOfWork.ProductCategories.GetAll().Select(s => new SelectListItem { Value = s.Code, Text = s.Name });

            #endregion
        }

        #endregion

        #region Update

        /// <summary>
        /// Update Product Group.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateProductGroup(ProductGroupViewModel productGroupViewModel)
        {
            #region Create Object to Update

            ProductGroup productGroup = new ProductGroup()
            {
                Id = productGroupViewModel.Id,
                Code = productGroupViewModel.Code,
                Name = productGroupViewModel.Name,
                UnitPrice = productGroupViewModel.UnitPrice,
                PromotionPrice = productGroupViewModel.PromotionPrice,
                MinQuantity = productGroupViewModel.MinQuantity,
                MaxQuantity = productGroupViewModel.MaxQuantity,
                ProductCategorieCode = productGroupViewModel.ProductCategorieCode,
                CreatedDateTime = productGroupViewModel.CreatedDateTime,
                UpdatedDateTime = DateTime.Now
            };

            #endregion

            _unitOfWork.ProductGroups.Update(productGroup);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete Product Group.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void DeleteProductGroup(ProductGroupViewModel productGroupViewModel)
        {
            #region Create Object to Delete

            ProductGroup productGroup = new ProductGroup()
            {
                Id = productGroupViewModel.Id,
            };

            #endregion

            _unitOfWork.ProductGroups.Remove(productGroup);
        }

        #endregion

        #endregion
    }
}
