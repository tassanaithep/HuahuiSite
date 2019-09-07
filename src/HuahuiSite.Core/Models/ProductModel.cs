using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Models
{
    public class ProductModel : Product
    {
        public string ProductCategorieName { get; set; }
        public string ProductGroupName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? PromotionPrice { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
    }
}
