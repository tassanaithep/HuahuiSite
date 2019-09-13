using AutoMapper;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Services.Class
{
    public class PromotionService :IPromotionService
    {
        #region Members

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public PromotionService
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

        #endregion

        #region Read

        /// <summary>
        /// Get Promotion List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 12/08/2019
        public void GetPromotionList(ref MainViewModel mainViewModel)
        {
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserSessionFrontend");

            mainViewModel.LoginViewModel = new LoginViewModel();

            if (loginViewModelSession != null)
            {
                mainViewModel.LoginViewModel = loginViewModelSession;
            }

            mainViewModel.PromotionViewModel = new PromotionViewModel();
            mainViewModel.PromotionViewModel.ProductList = _unitOfWork.Products.GetProductList().Where(x=>x.IsPromotion == true);
            mainViewModel.PromotionViewModel.ProductCategoriesList = _unitOfWork.ProductCategories.GetAll();

            if (mainViewModel.LoginViewModel.IsLogin)
            {
                mainViewModel.PromotionViewModel.CartItemListModelList = _unitOfWork.CartItemLists.GetCartItemListNotApproveByUser(mainViewModel.LoginViewModel.RoleId);
            }

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
           // mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();
        }

        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion

        #endregion
    }
}
