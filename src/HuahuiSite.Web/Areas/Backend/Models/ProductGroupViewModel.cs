using HuahuiSite.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Models
{
    public class ProductGroupViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int PromotionPrice { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public bool IsActive { get; set; }
        public string ProductCategorieCode { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public IEnumerable<SelectListItem> ProductCategorieSelectList { get; set; }

        public IEnumerable<ProductGroup> ProductGroupList { get; set; }
    }
}
