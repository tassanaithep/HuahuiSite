using AutoMapper;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserSessionFrontend");

            #region Save Cart and Order

            var cartOfUser = _unitOfWork.Carts.GetCartActiveByUser(loginViewModelSession.RoleId);

            Cart cart = new Cart();
            Order order = new Order();

            if (cartOfUser == null)
            {
                // Generate Order Id
                string orderId = GenerateOrderId();

                #region Save Cart

                #region Create Object to Save

                cart.OrderId = orderId;
                cart.UserRole = loginViewModelSession.RoleName;
                cart.UserId = loginViewModelSession.RoleId;
                cart.Status = "Cart";
                cart.IsActive = true;
                cart.CreatedDateTime = DateTime.Now;

                #endregion

                _unitOfWork.Carts.Add(cart);

                #endregion
            }

            #endregion

            #region Save Cart Item List

            CartItemListModel cartItemOfProduct = null;

            // If Already Cart
            if (cartOfUser != null)
            {
                // Get Cart Item List of Cart
                cartItemOfProduct = _unitOfWork.CartItemLists.GetCartItemListByCardAndProduct(cartOfUser.Id, cartViewModel.ProductId);
            }

            #region Save Cart Item List and Order Item List

            if (cartItemOfProduct == null)
            {
                #region Save Cart Item List

                #region Create Object to Save

                CartItemList cartItemList = new CartItemList()
                {
                    CardId = cartOfUser == null ? cart.Id : cartOfUser.Id,
                    ProductId = cartViewModel.ProductId,
                    Quantity = cartViewModel.QuantityOfItem,
                    TotalPrice = (!cartViewModel.ProductIsPromotion) ? (cartViewModel.ProductUnitPrice * cartViewModel.QuantityOfItem) : (cartViewModel.ProductPromotionPrice * cartViewModel.QuantityOfItem),
                    CreatedDateTime = DateTime.Now
                };

                #endregion

                _unitOfWork.CartItemLists.Add(cartItemList);

                #endregion
            }

            #endregion

            #region Update Cart Item List and Order Item List

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
                    CreatedDateTime = cartItemOfProduct.CreatedDateTime,
                    UpdatedDateTime = DateTime.Now
                };

                #endregion

                _unitOfWork.CartItemLists.Update(cartItemList);

                #endregion
            }

            #endregion

            #endregion
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
            #region Initial Login

            mainViewModel.LoginViewModel = new LoginViewModel();

            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserSessionFrontend");

            if (loginViewModelSession != null)
            {
                mainViewModel.LoginViewModel = loginViewModelSession;
            }

            #endregion

            #region Get Product Categorie List and Product Group List

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();

            #endregion

            #region Bind Data to Cart View Model

            mainViewModel.CartViewModel = Mapper.Map<Cart, CartViewModel>(_unitOfWork.Carts.GetCartActiveByUser(loginViewModelSession.RoleId));

            if (mainViewModel.CartViewModel != null)
            {
                mainViewModel.CartViewModel.CartItemModelList = _unitOfWork.CartItemLists.GetCartItemListByUser(loginViewModelSession.RoleId);

                #region Get Sale of Cart

                if (mainViewModel.CartViewModel.UserRole.Equals("Customer"))
                {
                    var customer = _unitOfWork.Customers.Get(mainViewModel.CartViewModel.UserId);

                    mainViewModel.CartViewModel.Sale = _unitOfWork.Sales.Get(customer.SaleId);
                }
                else if (mainViewModel.CartViewModel.UserRole.Equals("Sale"))
                {
                    if (mainViewModel.CartViewModel.Status.Equals("Cart"))
                    {
                        mainViewModel.CartViewModel.CustomerSelectList = _unitOfWork.Customers.GetAll().Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname + " " + s.Lastname });
                    }
                    else if (!mainViewModel.CartViewModel.Status.Equals("Cart"))
                    {
                        mainViewModel.CartViewModel.Customer = _unitOfWork.Customers.Get(mainViewModel.CartViewModel.CustomerId.Value);
                    }
                }

                #endregion
            }

            #endregion
        }

        #endregion

        #region Update

        /// <summary>
        /// Update Cart.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateCart(CartViewModel cartViewModel)
        {
            // Get Login User Data from Sesssion
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserSessionFrontend");

            #region Delete Old Cart Item List

            var cartOfUser = _unitOfWork.Carts.GetCartActiveByUser(loginViewModelSession.RoleId);
            var cartItemListToRemove = _unitOfWork.CartItemLists.GetCartItemListByCardId(cartOfUser.Id);
            _unitOfWork.CartItemLists.RemoveRange(cartItemListToRemove);

            #endregion

            #region Update Cart Item List

            if (cartViewModel.CartItemModelList.Count() > 0)
            {
                cartViewModel.CartItemModelList.ToList().ForEach(i => i.Id = 0);
                cartViewModel.CartItemModelList.ToList().ForEach(i => i.UpdatedDateTime = DateTime.Now);

                _unitOfWork.CartItemLists.AddRange(cartViewModel.CartItemModelList);
            }

            #endregion
        }

        public void CheckOut(int cartId, int customerId)
        {
            // Get Login User Data from Sesssion
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserSessionFrontend");

            if (loginViewModelSession != null)
            {
                #region Update Cart Status

                var cart = _unitOfWork.Carts.Get(cartId);

                if (loginViewModelSession.RoleName.Equals("Sale"))
                {
                    cart.CustomerId = customerId;
                }

                cart.Status = "Confirm";

                _unitOfWork.Carts.Update(cart);

                #endregion
            }
            else
            {
                throw new Exception("Session Timeout");
            }
        }

        #endregion

        #region Delete

        public void DeleteCartItem(int cartId, int cartItemId)
        {
            var cartItemList = _unitOfWork.CartItemLists.GetCartItemListByCardId(cartId);

            // If Cart Item List equal one item to Delete Cart
            if (cartItemList.Count().Equals(1))
            {
                #region Delete Cart

                var cart = _unitOfWork.Carts.Get(cartId);
                _unitOfWork.Carts.Remove(cart);

                #endregion
            }

            var cartItemToDelete = _unitOfWork.CartItemLists.Get(cartItemId);
            _unitOfWork.CartItemLists.Remove(cartItemToDelete);
        }

        #endregion

        #endregion

        #region Functions

        private string GenerateOrderId()
        {
            string orderId = string.Empty;

            orderId += "OR";
            orderId += DateTime.Now.ToString("yyyyMMdd");

            // Get Cart like Order Id
            var cartByOrderId = _unitOfWork.Carts.GetCartByLikeOrderId(orderId);

            // Get Order like Order Id
            var orderByOrderId = _unitOfWork.Orders.GetOrderByLikeOrderId(orderId);

            if (cartByOrderId.Count() > 0 && orderByOrderId.Count() > 0)
            {
                List<string> orderIdList = new List<string>();

                cartByOrderId.ToList().ForEach(i =>
                {
                    orderIdList.Add(i.OrderId);
                });

                orderByOrderId.ToList().ForEach(i =>
                {
                    orderIdList.Add(i.Id);
                });

                string lastOrderId = orderIdList.OrderByDescending(o => o).First();

                int lastOrderIdNumber = Convert.ToInt16(lastOrderId.Substring(lastOrderId.Length - 3));

                if (lastOrderIdNumber.ToString().Length == 1)
                {
                    orderId += "00" + (lastOrderIdNumber + 1).ToString();
                }
                else if (lastOrderIdNumber.ToString().Length == 2)
                {
                    orderId += "0" + (lastOrderIdNumber + 1).ToString();
                }
                else if (lastOrderIdNumber.ToString().Length == 3)
                {
                    orderId += (lastOrderIdNumber + 1).ToString();
                }
            }
            else
            {
                if (cartByOrderId.Count() == 0)
                {
                    if (orderByOrderId.Count() == 0)
                    {
                        orderId += "001";
                    }
                    else
                    {
                        string lastOrderId = orderByOrderId.OrderByDescending(o => o.Id).First().Id.ToString();
                        int lastOrderIdNumber = Convert.ToInt16(lastOrderId.Substring(lastOrderId.Length - 3));

                        if (lastOrderIdNumber.ToString().Length == 1)
                        {
                            orderId += "00" + (lastOrderIdNumber + 1).ToString();
                        }
                        else if (lastOrderIdNumber.ToString().Length == 2)
                        {
                            orderId += "0" + (lastOrderIdNumber + 1).ToString();
                        }
                        else if (lastOrderIdNumber.ToString().Length == 3)
                        {
                            orderId += (lastOrderIdNumber + 1).ToString();
                        }
                    }
                }
                else
                {
                    string lastOrderId = cartByOrderId.OrderByDescending(o => o.OrderId).First().OrderId.ToString();
                    int lastOrderIdNumber = Convert.ToInt16(lastOrderId.Substring(lastOrderId.Length - 3));

                    if (lastOrderIdNumber.ToString().Length == 1)
                    {
                        orderId += "00" + (lastOrderIdNumber + 1).ToString();
                    }
                    else if (lastOrderIdNumber.ToString().Length == 2)
                    {
                        orderId += "0" + (lastOrderIdNumber + 1).ToString();
                    }
                    else if (lastOrderIdNumber.ToString().Length == 3)
                    {
                        orderId += (lastOrderIdNumber + 1).ToString();
                    }
                }
            }

            return orderId;
        }

        #endregion
    }
}
