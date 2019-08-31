using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Models
{
    public class CartItemListModel : CartItemList
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string ProductName { get; set; }
        public string ProductGroupCode { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsPromotion { get; set; }
        public decimal PromotionPrice { get; set; }
        public string PictureFileName { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
    }
}
