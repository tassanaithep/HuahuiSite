﻿using System;
using System.Collections.Generic;

namespace HuahuiSite.Core.Entities
{
    public partial class Product
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
        public DateTime? UpdatedDateTime { get; set; }
    }
}
