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
        public void GetProductList(ref MainViewModel mainViewModel)
        {
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            mainViewModel.LoginViewModel = new LoginViewModel();

            if (loginViewModelSession != null)
            {
                mainViewModel.LoginViewModel = loginViewModelSession;
            }

            mainViewModel.HomeViewModel = new HomeViewModel();
            mainViewModel.HomeViewModel.ProductList = _unitOfWork.Products.GetProductList();

            if (mainViewModel.LoginViewModel.IsLogin)
            {
                mainViewModel.HomeViewModel.CartItemModelList = _unitOfWork.CartItemLists.GetCartItemListNotApproveByUser(mainViewModel.LoginViewModel.RoleId);
            }

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();
        }

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion

        #endregion
    }
}
