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
    public class ShopListService : IShopListService
    {
        #region Members

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor
        public ShopListService(IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        #endregion

        public void GetShopList(ref MainViewModel mainViewModel)
        {
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserSessionFrontend");

            mainViewModel.LoginViewModel = new LoginViewModel();

            if (loginViewModelSession != null)
            {
                mainViewModel.LoginViewModel = loginViewModelSession;
            }

            mainViewModel.ShopListViewModel = new ShopListViewModel();
            mainViewModel.ShopListViewModel.ProductList = _unitOfWork.Products.GetProductList();

            mainViewModel.ShopListViewModel.ProductCategoriesList = _unitOfWork.ProductCategories.GetAll();

            if (mainViewModel.LoginViewModel.IsLogin)
            {
                mainViewModel.ShopListViewModel.CartItemListModelList = _unitOfWork.CartItemLists.GetCartItemListNotApproveByUser(mainViewModel.LoginViewModel.RoleId);
            }

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();
        }
    }
}