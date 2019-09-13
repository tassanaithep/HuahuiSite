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
using System.Web.Helpers;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class CustomerService : ICustomerService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        /// <summary>
        /// Save Customer.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public int SaveCustomer(CustomerViewModel customerViewModel)
        {
            #region Create Object to Save

            Customer customer = new Customer()
            {
                Firstname = customerViewModel.Firstname,
                Lastname = customerViewModel.Lastname,
                Address = customerViewModel.Address,
                PhoneNumber = customerViewModel.PhoneNumber,
                Email = customerViewModel.Email,
                SaleId = customerViewModel.SaleId
            };

            #endregion

            _unitOfWork.Customers.Add(customer);

            return customer.Id;
        }

        #endregion

        #region Read

        /// <summary>
        /// Get Customer List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetCustomerList(ref CustomerViewModel customerViewModel)
        {
            customerViewModel.CustomerList = _unitOfWork.Customers.GetCustomerList();
        }

        #endregion

        #region Update

        /// <summary>
        /// Update Customer.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateCustomer(CustomerViewModel customerViewModel)
        {
            #region Update Customer

            #region Create Object to Update

            Customer customer = new Customer()
            {
                Id = customerViewModel.Id,
                Firstname = customerViewModel.Firstname,
                Lastname = customerViewModel.Lastname,
                Address = customerViewModel.Address,
                PhoneNumber = customerViewModel.PhoneNumber,
                Email = customerViewModel.Email,
                SaleId = customerViewModel.SaleId
            };

            #endregion

            _unitOfWork.Customers.Update(customer);

            #endregion

            #region Update Password Of User

            if (customerViewModel.IsChangePassword)
            {
                var user = _unitOfWork.Users.GetUserByRoleId(customerViewModel.Id);

                user.Password = Crypto.Hash(customerViewModel.NewPassword);

                _unitOfWork.Users.Update(user);
            }

            #endregion
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete Customer.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void DeleteCustomer(CustomerViewModel customerViewModel)
        {
            #region Delete Customer

            #region Create Object to Delete

            Customer customer = new Customer()
            {
                Id = customerViewModel.Id,
            };

            #endregion

            _unitOfWork.Customers.Remove(customer);

            #endregion

            #region Delete User of Customer

            var userOfCustomer = _unitOfWork.Users.GetUserByRole("Customer", customerViewModel.Id);
            _unitOfWork.Users.Remove(userOfCustomer);

            #endregion
        }

        #endregion

        #endregion
    }
}
