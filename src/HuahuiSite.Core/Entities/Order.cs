using System;
using System.Collections.Generic;

namespace HuahuiSite.Core.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
