using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string UserRole { get; set; }
        public int UserId { get; set; }
        public int? CustomerId { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        
        public Customer Customer { get; set; }
        public Sale Sale { get; set; }
        public IEnumerable<SelectListItem> CustomerSelectList { get; set; }

        #region Product

        public int ProductId { get; set; }
        public decimal  ProductUnitPrice { get; set; }
        public bool ProductIsPromotion { get; set; }
        public decimal ProductPromotionPrice { get; set; }

        #endregion

        public int QuantityOfItem { get; set; }

        public IEnumerable<CartItemListModel> CartItemModelList { get; set; }
    }
}
