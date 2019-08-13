using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string UserRole { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }

        #region Product

        public int ProductId { get; set; }
        public decimal  ProductUnitPrice { get; set; }

        #endregion

        public int QuantityOfItem { get; set; }

        public IEnumerable<CartItemListModel> CartItemModelList { get; set; }
    }
}
