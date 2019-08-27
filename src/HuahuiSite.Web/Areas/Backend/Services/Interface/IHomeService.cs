﻿using HuahuiSite.Web.Areas.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Backend.Services.Interface
{
    public interface IHomeService
    {
        void GetCompleteOrderList(ref HomeViewModel homeViewModel);
        void Search(ref HomeViewModel homeViewModel);
        void ExportToFile(ref byte[] result, string orderId);
    }
}
