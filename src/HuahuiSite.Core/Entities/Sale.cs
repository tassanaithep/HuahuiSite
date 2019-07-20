using System;
using System.Collections.Generic;

namespace HuahuiSite.Core.Entities
{
    public partial class Sale
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
