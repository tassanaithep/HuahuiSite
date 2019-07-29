using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class CartItemListViewModel : CartItemList
    {
        public string ProductName { get; set; }
        public int UnitPrice { get; set; }
        public string PictureFileName { get; set; }
    }
}
