using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using HuahuiSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IOrderedQueryable<Product> GetProductListData()
        {
            return HuahuiDbContext.Product.AsNoTracking().OrderBy(o => o.Id);
        }

        public IEnumerable<ProductModel> GetProductList()
        {
            return (from product in HuahuiDbContext.Product
                    join productGroup in HuahuiDbContext.ProductGroup on product.ProductGroupCode equals productGroup.Code
                    select new ProductModel
                    {
                        Id = product.Id,
                        Code = product.Code,
                        Name = product.Name,
                        UnitId = product.UnitId,
                        ProductCategorieCode = product.ProductCategorieCode,
                        ProductGroupCode = product.ProductGroupCode,
                        IsLicense = product.IsLicense,
                        IsPromotion = product.IsPromotion,
                        IsActive = product.IsActive,
                        PictureFileName = product.PictureFileName,
                        CreatedDateTime = product.CreatedDateTime,
                        UpdatedDateTime = product.UpdatedDateTime,
                        UnitPrice = productGroup.UnitPrice,
                        PromotionPrice = productGroup.PromotionPrice,
                        MinQuantity = productGroup.MinQuantity,
                        MaxQuantity = productGroup.MaxQuantity
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }
    }
}
