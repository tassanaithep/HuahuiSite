using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface ICartService
    {
        void GetCartList(ref CartViewModel cartViewModel);
        //int SaveCustomer(CustomerViewModel customerViewModel);
        //void UpdateCustomer(CustomerViewModel customerViewModel);
        //void DeleteCustomer(CustomerViewModel customerViewModel);
    }
}
