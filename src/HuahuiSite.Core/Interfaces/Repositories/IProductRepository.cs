﻿using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IOrderedQueryable<Product> GetProductListData();
        IEnumerable<ProductModel> GetProductList();
    }
}
