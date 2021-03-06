﻿using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface IOrderService
    {
        void GetOrderList(ref OrderViewModel orderViewModel, string keywordForSearch, bool isUpdate, int page);
        void CompleteOrder(string orderId);
        void CancelOrder(string orderId);
    }
}
