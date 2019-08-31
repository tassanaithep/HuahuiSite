using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Models
{
    public class HomeViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CustomerName { get; set; }
        public string SaleName { get; set; }

        #region Order

        public IEnumerable<OrderModel> OrderList { get; set; }
        public IEnumerable<OrderItemListModel> OrderItemList { get; set; }
        public IEnumerable<OrderItemListModel> CompleteOrderItemList { get; set; }

        #endregion

        #region Cart

        public IEnumerable<CartModel> CartList { get; set; }
        public IEnumerable<CartItemListModel> CartItemList { get; set; }

        #endregion
    }
}
