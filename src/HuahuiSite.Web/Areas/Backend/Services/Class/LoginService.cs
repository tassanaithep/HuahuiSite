using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using HuahuiSite.Web.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace HuahuiSite.Web.Areas.Backend.Services.Class
{
    public class LoginService : ILoginService
    {
        #region Members

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public LoginService(
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Check Login.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void CheckLogin(LoginViewModel loginViewModel)
        {
            var user = _unitOfWork.Users.GetUserOfLogin(loginViewModel.Username, Crypto.Hash(loginViewModel.Password));

            if (user.RoleName.Equals("Admin"))
            {
                Extensions.SessionExtensions.SetObject(_httpContextAccessor.HttpContext.Session, "UserSessionBackend", loginViewModel);
            }
            else
            {
                throw new Exception("Login Failed");
            }
        }

        /// <summary>
        /// Check Login Status.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public bool CheckLoginStatus()
        {
            var loginViewModel = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserSessionBackend");

            return loginViewModel != null ? true : false;
        }

        /// <summary>
        /// Logout.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void Logout()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserSessionBackend");
        }

        #endregion
    }
}
