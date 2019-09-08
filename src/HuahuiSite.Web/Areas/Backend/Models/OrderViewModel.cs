using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public IEnumerable<OrderItemListModel> OrderItemList { get; set; }

        public int StartNoOfTable { get; set; }
        public IOrderedQueryable<Order> OrderList { get; set; }
        public IPagingList<Order> OrderPagingList { get; set; }
    }
}
