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
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            #region Save Cart and Order

            var cartOfUser = _unitOfWork.Carts.GetCartActiveByUser(loginViewModelSession.RoleId);
            var orderOfUser = _unitOfWork.Orders.GetOrderActiveByUser(loginViewModelSession.RoleId);

            Cart cart = new Cart();
            Order order = new Order();

            if (cartOfUser == null)
            {
                #region Save Order

                // Generate Order Id
                int orderId = GenerateRandomNumber();

                #region Create Object to Save

                order.Id = orderId;
                order.UserRole = loginViewModelSession.RoleName;
                order.UserId = loginViewModelSession.RoleId;
                order.Status = "Cart";
                order.IsActive = true;
                order.CreatedDateTime = DateTime.Now;

                #endregion

                _unitOfWork.Orders.Add(order);

                #endregion

                #region Save Cart

                #region Create Object to Save

                cart.OrderId = order.Id;
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
            OrderItemListModel orderItemOfProduct = null;

            // If Already Cart
            if (cartOfUser != null)
            {
                // Get Cart Item List of Cart
                cartItemOfProduct = _unitOfWork.CartItemLists.GetCartItemListByCardAndProduct(cartOfUser.Id, cartViewModel.ProductId);

                // Get Order Item List of Order
                orderItemOfProduct = _unitOfWork.OrderItemLists.GetOrderItemListByCardAndProduct(cartOfUser.Id, cartViewModel.ProductId);
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
                    TotalPrice = (cartViewModel.ProductUnitPrice * cartViewModel.QuantityOfItem),
                    CreatedDateTime = DateTime.Now
                };

                #endregion

                _unitOfWork.CartItemLists.Add(cartItemList);

                #endregion

                #region Save Order Item List 

                #region Create Object to Save

                OrderItemList orderItemList = new OrderItemList()
                {
                    OrderId = cartOfUser == null ? order.Id : orderOfUser.Id,
                    ProductId = cartViewModel.ProductId,
                    Quantity = cartViewModel.QuantityOfItem,
                    TotalPrice = (cartViewModel.ProductUnitPrice * cartViewModel.QuantityOfItem),
                    CreatedDateTime = DateTime.Now
                };

                #endregion

                _unitOfWork.OrderItemLists.Add(orderItemList);

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

                #region Update Order Item List

                #region Create Object to Update

                OrderItemList orderItemList = new OrderItemList()
                {
                    Id = cartItemOfProduct.Id,
                    OrderId = orderItemOfProduct.Id,
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
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            mainViewModel.LoginViewModel = new LoginViewModel();

            if (loginViewModelSession != null)
            {
                mainViewModel.LoginViewModel = loginViewModelSession;
            }

            mainViewModel.ProductCategorieList = _unitOfWork.ProductCategories.GetAll();
            mainViewModel.ProductGroupList = _unitOfWork.ProductGroups.GetAll();

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
            #region Delete Old Cart Item List

            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            var cartOfUser = _unitOfWork.Carts.GetCartActiveByUser(loginViewModelSession.RoleId);
            var cartItemListToRemove = _unitOfWork.CartItemLists.GetCartItemListByCard(cartOfUser.Id);
            _unitOfWork.CartItemLists.RemoveRange(cartItemListToRemove);

            #endregion

            #region Delete Old Order Item List

            var orderOfUser = _unitOfWork.Orders.GetOrderActiveByUser(loginViewModelSession.RoleId);
            var orderItemListToRemove = _unitOfWork.OrderItemLists.GetOrderItemListByOrder(orderOfUser.Id);
            _unitOfWork.OrderItemLists.RemoveRange(orderItemListToRemove);

            #endregion

            #region Update Cart Item List

            if (cartViewModel.CartItemModelList.Count() > 0)
            {
                cartViewModel.CartItemModelList.ToList().ForEach(i => i.Id = 0);
                cartViewModel.CartItemModelList.ToList().ForEach(i => i.UpdatedDateTime = DateTime.Now);

                _unitOfWork.CartItemLists.AddRange(cartViewModel.CartItemModelList);
            }

            #endregion

            #region Update Order Item List

            if (cartViewModel.CartItemModelList.Count() > 0)
            {
                List<OrderItemList> orderItemList = new List<OrderItemList>();

                cartViewModel.CartItemModelList.ToList().ForEach(i => 
                {
                    orderItemList.Add(new OrderItemList
                    {
                        Id = 0,
                        OrderId = orderOfUser.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        TotalPrice = i.TotalPrice,
                        CreatedDateTime = i.CreatedDateTime,
                        UpdatedDateTime = DateTime.Now
                    });
                });

                _unitOfWork.OrderItemLists.AddRange(orderItemList);
            }

            #endregion
        }

        public void CheckOut(int cartId, int customerId)
        {
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            #region Update Cart Status

            var cart = _unitOfWork.Carts.Get(cartId);

            if (loginViewModelSession.RoleName.Equals("Sale"))
            {
                cart.CustomerId = customerId;
            }
        
            cart.Status = "Confirm";

            _unitOfWork.Carts.Update(cart);

            #endregion

            #region Update Order Status

            var order = _unitOfWork.Orders.Get(cart.OrderId);

            if (loginViewModelSession.RoleName.Equals("Sale"))
            {
                order.CustomerId = customerId;
            }

            order.Status = "Confirm";

            _unitOfWork.Orders.Update(order);

            #endregion
        }

        #endregion

        #region Delete

        #endregion

        #endregion

        #region Functions

        public int GenerateRandomNumber()
        {
            var random = new Random();
            string s = string.Empty;

            for (int i = 0; i < 6; i++)
            {
                s = String.Concat(s, random.Next(10).ToString());
            }

            return Convert.ToInt32(s);
        }

        #endregion
    }
}
