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
        bool CheckLoginStatus();
        void Logout();
    }
}
