using AutoMapper;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace HuahuiSite.Web.Areas.Frontend.Services.Class
{
    public class HomeService : IHomeService
    {
        #region Members

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public HomeService(
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

        #endregion

        #region Read

        /// <summary>
        /// Get Product List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetHome(ref MainViewModel mainViewModel)
        {
            #region Initial Login

            mainViewModel.LoginViewModel = new LoginViewModel();

            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            if (loginViewModelSession != null)
            {
                mainViewModel.LoginViewModel = loginViewModelSession;
            }

            #endregion

            #region Get Product Categorie List and Product Group List

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();

            #endregion

            #region Bind Data to Home View Model

            mainViewModel.HomeViewModel = new HomeViewModel();
            mainViewModel.HomeViewModel.ProductList = _unitOfWork.Products.GetProductList();

            #endregion

            if (mainViewModel.LoginViewModel.IsLogin)
            {
                mainViewModel.HomeViewModel.CartItemModelList = _unitOfWork.CartItemLists.GetCartItemListNotApproveByUser(mainViewModel.LoginViewModel.RoleId);
            }
        }

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion

        #endregion

        #region Actions

        public void GetProductPriceByQuantity(ref ProductGroupViewModel productGroup, string productGroupCode, int quantity)
        {
            productGroup = Mapper.Map<ProductGroup, ProductGroupViewModel>(_unitOfWork.ProductGroups.GetProductGroupByCodeAndQuantity(productGroupCode, quantity));
        }

        #endregion
    }
}
