using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface IUserService
    {
        void GetUserList(ref UserViewModel userViewModel);
        void SaveUser(UserViewModel userViewModel = null, SaleViewModel saleViewModel = null,  CustomerViewModel customerViewModel = null);
        void UpdateUser(UserViewModel userViewModel);
        void DeleteUser(UserViewModel userViewModel);
    }
}
