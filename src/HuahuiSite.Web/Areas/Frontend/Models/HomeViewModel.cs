using HuahuiSite.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class HomeViewModel
    {
        public bool IsLogin { get; set; }

        public IEnumerable<ProductViewModel> ProductList { get; set; }
        public IEnumerable<CartItemListViewModel> CartItemListViewList { get; set; }

        //public IEnumerable<ProductCategorie> ProductCategorieList { get; set; }
        //public IEnumerable<ProductGroup> ProductGroupList { get; set; }
    }

}
