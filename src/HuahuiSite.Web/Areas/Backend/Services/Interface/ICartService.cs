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
        void UpdateCart(CartViewModel cartViewModel);
        void DeleteCart(int cartId);
        void ApproveCart(int cartId);

        int GenerateRandomNumber();
    }
}
