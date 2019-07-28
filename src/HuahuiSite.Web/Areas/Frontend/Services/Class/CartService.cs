using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
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

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public CartService(IUnitOfWork unitOfWork)
        {
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
            #region Save Cart

            var cartOfUser = _unitOfWork.Carts.GetCartActiveByUser(2013);

            Cart cart = new Cart();

            if (cartOfUser == null)
            {
                #region Create Object to Save

                cart.UserRole = "Customer";
                cart.UserId = 2013;
                cart.Status = "Confirm";
                cart.IsActive = true;
                cart.CreatedDateTime = DateTime.Now;

                #endregion

                _unitOfWork.Carts.Add(cart);
            }

            #endregion

            #region Save Cart Item List

            #region Create Object to Save

            CartItemList cartItemList = new CartItemList()
            {
                CardId = cartOfUser == null ? cart.Id : cartOfUser.Id,
                ProductId = 3002,
                IsActive = true,
                CreatedDateTime = DateTime.Now
            };

            #endregion

            _unitOfWork.CartItemLists.Add(cartItemList);

            #endregion
        }

        #endregion

        #region Read

        /// <summary>
        /// Get Cart List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetCartItemList(ref CartViewModel cartViewModel)
        {
            var test = _unitOfWork.CartItemLists.GetCartListItemByUser(2013);
            var test2 = test;
        }

        #endregion

        //#region Update

        ///// <summary>
        ///// Update Cart.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //public void UpdateUser(UserViewModel userViewModel = null, SaleViewModel saleViewModel = null, CustomerViewModel customerViewModel = null)
        //{
        //    var decryptPassword = Crypto.HashPassword(customerViewModel.Password);
        //    #region Create Object to Update

        //    Cart user = new Cart();

        //    if (userViewModel != null)
        //    {
        //        user.Id = userViewModel.Id;
        //        user.Name = userViewModel.Name;
        //        user.Username = userViewModel.Username;
        //        user.Password = userViewModel.Password;
        //    }
        //    else if (saleViewModel != null)
        //    {
        //        user.Id = saleViewModel.UserId;
        //        user.Name = saleViewModel.Firstname + " " + saleViewModel.Lastname;
        //        user.Username = saleViewModel.Username;
        //        user.Password = saleViewModel.Password;
        //    }
        //    else if (customerViewModel != null)
        //    {
        //        user.Id = customerViewModel.Id;
        //        user.Name = customerViewModel.Firstname + " " + customerViewModel.Lastname;
        //        user.Username = customerViewModel.Username;
        //        user.Password = customerViewModel.Password;
        //    }

        //    #endregion

        //    _unitOfWork.Users.Update(user);
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

        #endregion
    }
}
