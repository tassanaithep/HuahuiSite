using HuahuiSite.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string TitleNameEn { get; set; }
        public string TitleNameTh { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionTh { get; set; }
        public string TitlePictureFileName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public IFormFile TitlePictureImageFile { get; set; }

        public IEnumerable<Product> ProductList { get; set; }
    }
}
