using System;
using System.Collections.Generic;

namespace HuahuiSite.Core.Entities
{
    public partial class ProductGroup
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int? PromotionPrice { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public bool IsActive { get; set; }
        public string ProductCategorieCode { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
