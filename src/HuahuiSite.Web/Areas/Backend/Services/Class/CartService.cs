using AutoMapper;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Core.Models;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
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

        //#region Create

        ///// <summary>
        ///// Save Customer.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //public int SaveCustomer(CustomerViewModel customerViewModel)
        //{
        //    #region Create Object to Save

        //    Customer customer = new Customer()
        //    {
        //        Firstname = customerViewModel.Firstname,
        //        Lastname = customerViewModel.Lastname,
        //        Address = customerViewModel.PhoneNumber,
        //        PhoneNumber = customerViewModel.PhoneNumber,
        //        Email = customerViewModel.Email,
        //        SaleId = customerViewModel.SaleId
        //    };

        //    #endregion

        //    _unitOfWork.Customers.Add(customer);

        //    return customer.Id;
        //}

        //#endregion

        #region Read

        /// <summary>
        /// Get Cart List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetCartList(ref CartViewModel cartViewModel)
        {
            cartViewModel.CartList = _unitOfWork.Carts.GetCartList();
            cartViewModel.CartItemList = _unitOfWork.CartItemLists.GetCartItemList();
        }

        #endregion

        //#region Update

        ///// <summary>
        ///// Update Customer.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //public void UpdateCustomer(CustomerViewModel customerViewModel)
        //{
        //    #region Create Object to Update

        //    Customer customer = new Customer()
        //    {
        //        Id = customerViewModel.Id,
        //        Firstname = customerViewModel.Firstname,
        //        Lastname = customerViewModel.Lastname,
        //        Address = customerViewModel.Address,
        //        PhoneNumber = customerViewModel.PhoneNumber,
        //        Email = customerViewModel.Email,
        //        SaleId = customerViewModel.SaleId
        //    };

        //    #endregion

        //    _unitOfWork.Customers.Update(customer);
        //}

        //#endregion

        //#region Delete

        ///// <summary>
        ///// Delete Customer.
        ///// </summary>
        //// Author: Mod Nattasit
        //// Updated: 07/07/2019
        //public void DeleteCustomer(CustomerViewModel customerViewModel)
        //{
        //    #region Create Object to Delete

        //    Customer customer = new Customer()
        //    {
        //        Id = customerViewModel.Id,
        //    };

        //    #endregion

        //    _unitOfWork.Customers.Remove(customer);
        //}

        //#endregion

        #endregion
    }
}
