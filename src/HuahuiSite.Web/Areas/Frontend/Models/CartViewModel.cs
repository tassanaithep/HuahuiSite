using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string UserRole { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public IEnumerable<CartItemList> CartItemList { get; set; }
        public IEnumerable<CartItemListViewModel> CartItemListViewList { get; set; }
    }
}
