using System;
using System.Collections.Generic;

namespace HuahuiSite.Core.Entities
{
    public partial class CartItemList
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
