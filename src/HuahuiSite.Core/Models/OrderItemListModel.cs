using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Models
{
    public class OrderItemListModel : OrderItemList
    {
        public string ProductName { get; set; }
        public string ProductGroupCode { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsPromotion { get; set; }
        public string PictureFileName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string SaleName { get; set; }
    }
}
