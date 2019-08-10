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
    public class CartService : ICartService
    {
        #region Members

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public CartService(
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

        /// <summary>
        /// Save Cart.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void SaveCart(CartViewModel cartViewModel)
        {
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            #region If Not Already Cart

            #region Save Cart

            var cartOfUser = _unitOfWork.Carts.GetCartActiveByUser(loginViewModelSession.RoleId);

            Cart cart = new Cart();

            if (cartOfUser == null)
            {
                #region Create Object to Save

                cart.UserRole = loginViewModelSession.RoleName;
                cart.UserId = loginViewModelSession.RoleId;
                cart.Status = "Cart";
                cart.IsActive = true;
                cart.CreatedDateTime = DateTime.Now;

                #endregion

                _unitOfWork.Carts.Add(cart);
            }

            #endregion

            #endregion

            CartItemListModel cartItemOfProduct = null;

            if (cartOfUser != null)
            {
                cartItemOfProduct = _unitOfWork.CartItemLists.GetCartItemListByCardAndProduct(cartOfUser.Id, cartViewModel.ProductId);
            }

            if (cartItemOfProduct == null)
            {
                #region Save Cart Item List

                #region Create Object to Save

                CartItemList cartItemList = new CartItemList()
                {
                    CardId = cartOfUser == null ? cart.Id : cartOfUser.Id,
                    ProductId = cartViewModel.ProductId,
                    Quantity = cartViewModel.QuantityOfItem,
                    TotalPrice = (cartViewModel.ProductUnitPrice * cartViewModel.QuantityOfItem),
                    IsActive = true,
                    CreatedDateTime = DateTime.Now
                };

                #endregion

                _unitOfWork.CartItemLists.Add(cartItemList);

                #endregion
            }
            else
            {
                #region Update Cart Item List

                #region Create Object to Update

                CartItemList cartItemList = new CartItemList()
                {
                    Id = cartItemOfProduct.Id,
                    CardId = cartOfUser.Id,
                    ProductId = cartViewModel.ProductId,
                    Quantity = cartViewModel.QuantityOfItem,
                    TotalPrice = (cartViewModel.ProductUnitPrice * cartViewModel.QuantityOfItem),
                    IsActive = cartItemOfProduct.IsActive,
                    CreatedDateTime = cartItemOfProduct.CreatedDateTime,
                    UpdatedDateTime = DateTime.Now
                };

                #endregion

                _unitOfWork.CartItemLists.Update(cartItemList);

                #endregion
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// Get Cart List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetCartItemList(ref MainViewModel mainViewModel)
        {
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            mainViewModel.LoginViewModel = new LoginViewModel();

            if (loginViewModelSession != null)
            {
                mainViewModel.LoginViewModel = loginViewModelSession;
            }

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();

            mainViewModel.CartViewModel = new CartViewModel();
            mainViewModel.CartViewModel.CartItemListViewList = Mapper.Map<IEnumerable<CartItemListModel>, IEnumerable<CartItemListViewModel>>(_unitOfWork.CartItemLists.GetCartItemListByUser(loginViewModelSession.RoleId));
            mainViewModel.CartViewModel.CartItemList = _unitOfWork.CartItemLists.GetCartItemListByCard(mainViewModel.CartViewModel.CartItemListViewList.First().CardId);
        }

        #endregion

        #region Update

        ///// <summary>
        ///// Update Cart.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        public void UpdateCart(CartViewModel cartViewModel)
        {
            #region Delete Old Cart Item List

            var cartItemListToRemove = _unitOfWork.CartItemLists.GetCartItemListByCard(cartViewModel.CartItemList.First().CardId);
            _unitOfWork.CartItemLists.RemoveRange(cartItemListToRemove);

            #endregion

            #region Update Cart Item List

            cartViewModel.CartItemList.ToList().ForEach(i => i.Id = 0);

            _unitOfWork.CartItemLists.AddRange(cartViewModel.CartItemList);

            #endregion
        }

        public void CheckOut(int cartId)
        {
            var cart = _unitOfWork.Carts.Get(cartId);

            cart.Status = "Confirm";

            _unitOfWork.Carts.Update(cart);
        }

        #endregion

        #region Delete

        #endregion

        #endregion
    }
}
