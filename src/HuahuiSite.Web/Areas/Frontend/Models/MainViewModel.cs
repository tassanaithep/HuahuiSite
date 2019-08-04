using HuahuiSite.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class MainViewModel
    {
        public IEnumerable<ProductCategorie> ProductCategorieList { get; set; }
        public IEnumerable<ProductGroup> ProductGroupList { get; set; }

        public LoginViewModel LoginViewModel { get; set; }
        public HomeViewModel HomeViewModel { get; set; }
        public CartViewModel CartViewModel { get; set; }
        public ShopListViewModel ShopListViewModel { get; set; }
    }
}
