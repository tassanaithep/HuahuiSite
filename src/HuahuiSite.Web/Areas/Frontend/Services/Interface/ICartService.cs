using HuahuiSite.Web.Areas.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Services.Interface
{
    public interface ICartService
    {
        void SaveCart(CartViewModel cartViewModel);
        void GetCartItemList(ref MainViewModel mainViewModel);
        void UpdateCart(CartViewModel cartViewModel);
        void DeleteCartItem(int cartId, int cartItemId);
        void CheckOut(int cartId, int customerId);
    }
}
