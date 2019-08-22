using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; }
        public IEnumerable<CartItemListModel> CartItemModelList { get; set; }
    }
}
