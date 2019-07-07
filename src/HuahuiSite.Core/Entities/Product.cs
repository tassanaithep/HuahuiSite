using System;
using System.Collections.Generic;

namespace HuahuiSite.Core.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string TitleNameEn { get; set; }
        public string TitleNameTh { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionTh { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
