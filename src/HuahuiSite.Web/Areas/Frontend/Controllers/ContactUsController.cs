using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HuahuiSite.Web.Areas.Frontend.Controllers
{
    [Area("Frontend")]
    public class ContactUsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public ContactUsController(IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            MainViewModel mainViewModel = new MainViewModel();

            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            mainViewModel.LoginViewModel = new LoginViewModel();
            mainViewModel.LoginViewModel.IsLogin = loginViewModelSession != null ? true : false;
            mainViewModel.HomeViewModel = new HomeViewModel();
            mainViewModel.HomeViewModel.ProductList = Mapper.Map<IEnumerable<ProductModel>, IEnumerable<ProductViewModel>>(_unitOfWork.Products.GetProductList());

            if (mainViewModel.LoginViewModel != null)
            {
                mainViewModel.HomeViewModel.CartItemModelList = _unitOfWork.CartItemLists.GetCartItemListByUser(mainViewModel.LoginViewModel.RoleId);
            }

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();

            return View(mainViewModel);
        }
    }
}