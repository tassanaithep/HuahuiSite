using System;
using System.Collections.Generic;

namespace HuahuiSite.Core.Entities
{
    public partial class UnitOfProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
