using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Models
{
    public class ProductModel : Product
    {
        public int UnitPrice { get; set; }
        public int? PromotionPrice { get; set; }
    }
}
