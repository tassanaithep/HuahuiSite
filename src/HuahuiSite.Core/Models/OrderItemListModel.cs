using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Models
{
    public class OrderItemListModel : OrderItemList
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PictureFileName { get; set; }
    }
}
