using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public string ProductCategorieCode { get; set; }
        public string ProductGroupCode { get; set; }
        public bool IsLicense { get; set; }
        public bool IsPromotion { get; set; }
        public bool IsActive { get; set; }
        public string PictureFileName { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public IFormFile PictureFile { get; set; }
        public bool IsRemoveImage { get; set; }

        public IEnumerable<SelectListItem> UnitOfProductSelectList { get; set; }
        public IEnumerable<SelectListItem> ProductCategorieSelectList { get; set; }
        public IEnumerable<SelectListItem> ProductGroupSelectList { get; set; }

        public int StartNoOfTable { get; set; }
        public IOrderedQueryable<ProductModel> ProductList { get; set; }
        public IPagingList<ProductModel> ProductPagingList { get; set; }
    }
}
