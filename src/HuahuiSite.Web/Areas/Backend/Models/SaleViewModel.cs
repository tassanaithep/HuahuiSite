using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Models
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Mon สร้าง 22/7/62
        public string Username { get; set; }
        public string Password { get; set; }

        // Mon สร้าง 22/7/62

        public IEnumerable<Sale> SaleList { get; set; }
    }
}
