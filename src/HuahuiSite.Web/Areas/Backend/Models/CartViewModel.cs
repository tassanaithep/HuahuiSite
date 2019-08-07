using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string UserRole { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public IEnumerable<CartModel> CartList { get; set; }
        public IEnumerable<CartItemListModel> CartItemList { get; set; }
    }
}
