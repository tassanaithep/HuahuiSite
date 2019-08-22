using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class PromotionViewModel
    {
        public bool IsLogin { get; set; }

        public IEnumerable<ProductModel> ProductList { get; set; }
        public IEnumerable<ProductCategorie> ProductCategoriesList { get; set; }
        public IEnumerable<CartItemListViewModel> CartItemListViewList { get; set; }
    }
}
