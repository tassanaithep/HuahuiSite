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
    public class UserService : IUserService
    {
        #region Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region CRUD

        #region Create

        /// <summary>
        /// Save User.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void SaveUser(UserViewModel userViewModel = null, SaleViewModel saleViewModel = null,  CustomerViewModel customerViewModel = null)
        {
            #region Create Object to Save

            User user = new User();

            if (userViewModel != null)
            {
                user.Name = userViewModel.Name;
                user.Username = userViewModel.Username;
                user.Password = userViewModel.Password;
                user.RoleName = "Admin";
            }
            else if (saleViewModel != null)
            {
                user.Name = saleViewModel.Firstname + " " + saleViewModel.Lastname;
                user.Username = saleViewModel.Username;
                user.Password = saleViewModel.Password;
                user.RoleName = "Sale";
            }
            else if (customerViewModel != null)
            {
                user.Name = customerViewModel.Firstname + " " + customerViewModel.Lastname;
                user.Username = customerViewModel.Username;
                user.Password = customerViewModel.Password;
                user.RoleName = "Customer";
            }

            #endregion

            _unitOfWork.Users.Add(user);
        }

        #endregion

        #region Read

        /// <summary>
        /// Get User List.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void GetUserList(ref UserViewModel userViewModel)
        {
            userViewModel.UserList = _unitOfWork.Users.GetAll();
        }

        #endregion

        #region Update

        /// <summary>
        /// Update User.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void UpdateUser(UserViewModel userViewModel)
        {
            #region Create Object to Update

            User user = new User()
            {
                Id = userViewModel.Id,
                Name = userViewModel.Name,
                Username = userViewModel.Username,
                Password = userViewModel.Password,
                RoleName = userViewModel.RoleName
            };

            #endregion

            _unitOfWork.Users.Update(user);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete User.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void DeleteUser(UserViewModel userViewModel)
        {
            #region Create Object to Delete

            User user = new User()
            {
                Id = userViewModel.Id,
            };

            #endregion

            _unitOfWork.Users.Remove(user);
        }

        #endregion

        #endregion
    }
}
