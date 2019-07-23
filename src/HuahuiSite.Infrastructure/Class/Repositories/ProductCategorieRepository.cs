using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class ProductCategorieRepository : Repository<ProductCategorie>, IProductCategorieRepository
    {
        public ProductCategorieRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }
    }
}
