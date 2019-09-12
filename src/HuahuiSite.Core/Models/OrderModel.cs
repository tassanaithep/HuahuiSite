using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Models
{
    public class OrderModel : Order
    {
        public string CustomerName { get; set; }
        public string SaleName { get; set; }
        public string Username { get; set; }
    }
}
