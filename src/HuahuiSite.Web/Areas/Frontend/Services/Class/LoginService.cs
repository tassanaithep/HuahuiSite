using HuahuiSite.Core.Interfaces;
using HuahuiSite.Web.Areas.Frontend.Models;
using HuahuiSite.Web.Areas.Frontend.Services.Interface;
using HuahuiSite.Web.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Services.Class
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
            var user = _unitOfWork.Users.GetUserOfLogin(loginViewModel.Username, loginViewModel.Password);

            if (user.RoleName.Equals("Sale") || user.RoleName.Equals("Customer") || user.RoleName.Equals("Admin"))
            {
                loginViewModel.IsLogin = true;
                loginViewModel.Username = user.Username;
                loginViewModel.Password = user.Password;
                loginViewModel.RoleName = user.RoleName;
                loginViewModel.RoleId = user.RoleId.Value;
                loginViewModel.Name = user.Name;

                Extensions.SessionExtensions.SetObject(_httpContextAccessor.HttpContext.Session, "UserDataSession", loginViewModel);
            }
            else
            {
                throw new Exception("Login Failed");
            }
        }

        /// <summary>
        /// Initial Login.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public void InitialLoginStatus(ref LoginViewModel loginViewModel)
        {
            var loginViewModelSession = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

            loginViewModel = new LoginViewModel();
            loginViewModel.IsLogin = loginViewModelSession != null ? true : false;
        }

        /// <summary>
        /// Check Login Status.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        public bool CheckLoginStatus()
        {
            var loginViewModel = Extensions.SessionExtensions.GetObject<LoginViewModel>(_httpContextAccessor.HttpContext.Session, "UserDataSession");

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
            _httpContextAccessor.HttpContext.Session.Remove("UserDataSession");
        }
    }
}
