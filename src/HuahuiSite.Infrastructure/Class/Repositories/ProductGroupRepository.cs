using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class ProductGroupRepository : Repository<ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }

        public ProductGroup GetProductGroupByCodeAndQuantity(string code, int quantity)
        {
            return HuahuiDbContext.ProductGroup.Where(w => w.Code.Equals(code) && w.MinQuantity <= quantity && w.MaxQuantity >= quantity).SingleOrDefault();
        }
        public IEnumerable<ProductGroup> GetBySearch(string keywordForSearch)
        {
            return HuahuiDbContext.ProductGroup.Where(x => x.Name.Contains(keywordForSearch) || x.Code.Contains(keywordForSearch) || x.ProductCategorieCode.Contains(keywordForSearch)).ToList();
        }
      
    }
}
