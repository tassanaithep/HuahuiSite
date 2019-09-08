using HuahuiSite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface IProductGroupRepository : IRepository<ProductGroup>
    {
        ProductGroup GetProductGroupByCodeAndQuantity(string code, int quantity);
        IEnumerable<ProductGroup> GetBySearch(string keywordForSearch);

    }
}
