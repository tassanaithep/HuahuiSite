using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Models
{
    public class CustomerModel : Customer
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
