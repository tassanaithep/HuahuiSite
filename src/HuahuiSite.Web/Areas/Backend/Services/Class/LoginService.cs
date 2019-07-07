using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Backend.Models;
using HuahuiSite.Web.Areas.Backend.Services.Interface;
using HuahuiSite.Web.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Check Login.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void CheckLogin(LoginViewModel loginViewModel)
        {
            _unitOfWork.Users.GetUser(loginViewModel.Username, loginViewModel.Password);

            Extensions.SessionExtensions.SetObject(_httpContextAccessor.HttpContext.Session, "UserData", loginViewModel);
        }

        /// <summary>
        /// Check Login Status.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public bool CheckLoginStatus()
        {
            var loginViewModel = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserData");

            if (loginViewModel != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Logout.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void Logout()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserData");
        }
    }
}
