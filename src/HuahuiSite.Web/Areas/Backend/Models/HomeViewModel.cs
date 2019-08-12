﻿using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Models
{
    public class HomeViewModel
    {
        public IEnumerable<OrderModel> CompleteOrderList { get; set; }
        public IEnumerable<OrderItemListModel> CompleteOrderItemList { get; set; }
    }
}
