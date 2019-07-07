using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface ILoginService
    {
        void CheckLogin(LoginViewModel loginViewModel);

        /// <summary>
        /// Check Login Status.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        bool CheckLoginStatus();

        /// <summary>
        /// Logout.
        /// </summary>
        // Author: Mod Nattasit
        // Updated: 07/07/2019
        void Logout();
    }
}
