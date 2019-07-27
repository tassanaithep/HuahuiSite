using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        public void SaveCustomer(CustomerViewModel customerViewModel)
        {
            #region Create Object to Save

            Customer Customer = new Customer()
            {
                Firstname = customerViewModel.Firstname,
                Lastname = customerViewModel.Lastname,
                Address = customerViewModel.PhoneNumber,
                PhoneNumber = customerViewModel.PhoneNumber,
                Email = customerViewModel.Email,
                SaleId = customerViewModel.SaleId
            };

            #endregion

            _unitOfWork.Customers.Add(Customer);
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
            customerViewModel.CustomerList = _unitOfWork.Customers.GetAll();
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
            #region Create Object to Update

            Customer Customer = new Customer()
            {
                Id = customerViewModel.Id,
                Firstname = customerViewModel.Firstname,
                Lastname = customerViewModel.Lastname,
                Address = customerViewModel.Address,
                PhoneNumber = customerViewModel.PhoneNumber,
                Email = customerViewModel.Email
            };

            #endregion

            _unitOfWork.Customers.Update(Customer);
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
            #region Create Object to Delete

            Customer Customer = new Customer()
            {
                Id = customerViewModel.Id,
            };

            #endregion

            _unitOfWork.Customers.Remove(Customer);
        }

        #endregion

        #endregion
    }
}
