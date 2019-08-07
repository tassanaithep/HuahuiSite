using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Models
{
    public class PromotionViewModel
    {
        public bool IsLogin { get; set; }

        public IEnumerable<ProductViewModel> ProductList { get; set; }
        public IEnumerable<ProductCategorie> ProductCategoriesList { get; set; }
        // public IEnumerable<ProductGroup> ProductGroupList { get; set; }
      //  public string Param { get; set; }
      //  public string Param2 { get; set; }



    }
}
