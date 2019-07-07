using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }
    }
}
