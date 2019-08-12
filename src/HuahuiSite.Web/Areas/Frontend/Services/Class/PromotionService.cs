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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public PromotionService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public void GetShopList(ref MainViewModel mainViewModel)
        {
            //var loginViewModel = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserData");

            //mainViewModel.LoginViewModel.IsLogin = loginViewModel != null ? true : false;
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            mainViewModel.LoginViewModel = new LoginViewModel();
            mainViewModel.LoginViewModel.IsLogin = loginViewModelSession != null ? true : false;

            mainViewModel.PromotionViewModel = new PromotionViewModel();
            mainViewModel.PromotionViewModel.ProductList = Mapper.Map<IEnumerable<ProductModel>, IEnumerable<ProductViewModel>>(_unitOfWork.Products.GetProductList());
            //  mainViewModel.HomeViewModel.ProductList = Mapper.Map<IEnumerable<ProductModel>, IEnumerable<ProductViewModel>>(_unitOfWork.Products.GetProductList());

            mainViewModel.PromotionViewModel.ProductCategoriesList = _unitOfWork.ProductCategories.GetAll();

    

            if (mainViewModel.LoginViewModel.IsLogin)
            {
                mainViewModel.PromotionViewModel.CartItemListViewList = Mapper.Map<IEnumerable<CartItemListModel>, IEnumerable<CartItemListViewModel>>(_unitOfWork.CartItemLists.GetCartItemListByUser(mainViewModel.LoginViewModel.RoleId));
            }

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();

        }
    }


}
