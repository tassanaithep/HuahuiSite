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

        //#region Create

        ///// <summary>
        ///// Save Cart.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //public void SaveCart(CartViewModel cartViewModel)
        //{
        ////    #region Save Cart

        //    var cartOfUser = _unitOfWork.Carts.GetCartActiveByUser(2013);

        ////    Cart cart = new Cart();

        ////    if (cartOfUser == null)
        ////    {
        ////        #region Create Object to Save

        ////        cart.UserRole = "Customer";
        ////        cart.UserId = 2013;
        ////        cart.Status = "Confirm";
        ////        cart.IsActive = true;
        ////        cart.CreatedDateTime = DateTime.Now;

        ////        #endregion

        ////        _unitOfWork.Carts.Add(cart);
        ////    }

        ////    #endregion

        ////    #region Save Cart Item List

        ////    #region Create Object to Save

        //CartItemList cartItemList = new CartItemList()
        //{
        //    CardId = cartOfUser == null ? cart.Id : cartOfUser.Id,
        //    ProductId = 3002,
        //    Quantity = 1,
        //    TotalPrice = 500,
        //    IsActive = true,
        //    CreatedDateTime = DateTime.Now
        //};

        ////    #endregion

        ////    _unitOfWork.CartItemLists.Add(cartItemList);

        ////    #endregion
        //}

        ////#endregion

        //#region Read

        /// <summary>
        /// Get Product List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetProductList(ref HomeViewModel homeViewModel)
        {
            var loginViewModel = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserData");

            homeViewModel.IsLogin = loginViewModel != null ? true : false;
            homeViewModel.ProductList = Mapper.Map<IEnumerable<ProductModel>, IEnumerable<ProductViewModel>>(_unitOfWork.Products.GetProductList());

            homeViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            homeViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();
          


    }

        #endregion

        //#region Update

        /////// <summary>
        /////// Update Cart.
        /////// </summary>
        ////// Author: Mod Nattasit
        ////// Updated: 07/07/2019
        //public void UpdateCart(CartViewModel cartViewModel)
        //{
        //    #region Delete Old Cart Item List

        //    var cartItemListToRemove = _unitOfWork.CartItemLists.GetCartItemListByCard(cartViewModel.CartItemList.First().CardId);
        //    _unitOfWork.CartItemLists.RemoveRange(cartItemListToRemove);

        //    #endregion


        //    #region Update Cart Item List

        //    _unitOfWork.CartItemLists.AddRange(cartViewModel.CartItemList);

        //    #endregion
        //}

        //#endregion

        //#region Delete

        ///// <summary>
        ///// Delete Cart.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //public void DeleteUser(UserViewModel userViewModel)
        //{
        //    #region Create Object to Delete

        //    Cart user = new Cart()
        //    {
        //        Id = userViewModel.Id,
        //    };

        //    #endregion

        //    _unitOfWork.Users.Remove(user);
        //}

        //#endregion

        //#endregion
    }
}
