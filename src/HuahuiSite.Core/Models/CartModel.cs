using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Models
{
    public class CartModel : Cart
    {
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public string SaleName { get; set; }
    }
}
