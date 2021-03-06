﻿using HuahuiSite.Core.Entities;
using HuahuiSite.Web.Areas.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuahuiSite.Web.Areas.Frontend.Services.Interface
{
    public interface IHomeService
    {
        void GetHome(ref MainViewModel mainViewModel);
        void GetProductPriceByQuantity(ref ProductGroupViewModel productGroup, string productGroupCode, int quantity);
    }
}
