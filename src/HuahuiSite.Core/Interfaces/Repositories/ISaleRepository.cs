﻿using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface ISaleRepository : IRepository<Sale>
    {
        IEnumerable<SaleModel> GetSaleList();
    }
}
