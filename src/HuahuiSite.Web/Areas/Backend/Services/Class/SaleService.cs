using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class SaleService : ISaleService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public SaleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        /// <summary>
        /// Save Sale.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public int SaveSale(SaleViewModel saleViewModel)
        {
            #region Create Object to Save

            Sale sale = new Sale()
            {
                Firstname = saleViewModel.Firstname,
                Lastname = saleViewModel.Lastname,
                Address = saleViewModel.Address,
                PhoneNumber = saleViewModel.PhoneNumber,
                Email = saleViewModel.Email
            };

            #endregion

            _unitOfWork.Sales.Add(sale);

            return sale.Id;
        }

        #endregion

        #region Read

        /// <summary>
        /// Get Sale List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetSaleList(ref SaleViewModel saleViewModel)
        {
            saleViewModel.SaleList = _unitOfWork.Sales.GetSaleList();
        }

        /// <summary>
        /// Get Sale Select List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetSaleSelectList(ref CustomerViewModel customerViewModel)
        {
            customerViewModel.SaleSelectList = _unitOfWork.Sales.GetAll().Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Firstname+"  "+s.Lastname });
        }

        #endregion

        #region Update

        /// <summary>
        /// Update Sale.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateSale(SaleViewModel saleViewModel)
        {
            #region Update Sale

            #region Create Object to Update

            Sale sale = new Sale()
            {
                Id = saleViewModel.Id,
                Firstname = saleViewModel.Firstname,
                Lastname = saleViewModel.Lastname,
                Address = saleViewModel.Address,
                PhoneNumber = saleViewModel.PhoneNumber,
                Email = saleViewModel.Email
            };

            #endregion

            _unitOfWork.Sales.Update(sale);

            #endregion

            #region Update Password Of User

            if (saleViewModel.IsChangePassword)
            {
                var user = _unitOfWork.Users.GetUserByRoleId(saleViewModel.Id);

                user.Password = Crypto.Hash(saleViewModel.NewPassword);

                _unitOfWork.Users.Update(user);
            }

            #endregion
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete Sale.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void DeleteSale(SaleViewModel saleViewModel)
        {
            #region Delete Sale

            #region Create Object to Delete

            Sale sale = new Sale()
            {
                Id = saleViewModel.Id,
            };

            #endregion

            _unitOfWork.Sales.Remove(sale);

            #endregion

            #region Delete User of Sale

            var userOfCustomer = _unitOfWork.Users.GetUserByRole("Customer", saleViewModel.Id);
            _unitOfWork.Users.Remove(userOfCustomer);

            #endregion
        }

        #endregion

        #endregion
    }
}
