using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
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

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsChangePassword { get; set; }
        public string NewPassword { get; set; }

        public IEnumerable<SaleModel> SaleList { get; set; }
    }
}
