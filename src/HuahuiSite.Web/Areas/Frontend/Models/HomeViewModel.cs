using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class HomeViewModel
    {
        //public IEnumerable<ProductCategorie> ProductCategorieList { get; set; }
        public IEnumerable<ProductViewModel> ProductList { get; set; }
        public IEnumerable<CartItemListViewModel> CartItemListViewList { get; set; }
    }
}
