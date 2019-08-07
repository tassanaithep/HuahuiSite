using HuahuiSite.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IEnumerable<Order> OrderList { get; set; }
    }
}
