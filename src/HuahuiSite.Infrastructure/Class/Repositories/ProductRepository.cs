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

        public IOrderedQueryable<ProductModel> GetProductListData(string keywordForSearch)
        {
            return (from product in HuahuiDbContext.Product.AsNoTracking()
                    join productCategorie in HuahuiDbContext.ProductCategorie.AsNoTracking() on product.ProductCategorieCode equals productCategorie.Code
                    join productGroup in HuahuiDbContext.ProductGroup.AsNoTracking() on product.ProductGroupCode equals productGroup.Code
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
                        ProductGroupName = productGroup.Name,
                        UnitPrice = productGroup.UnitPrice,
                        PromotionPrice = productGroup.PromotionPrice,
                        MinQuantity = productGroup.MinQuantity,
                        MaxQuantity = productGroup.MaxQuantity,
                        ProductCategorieName = productCategorie.Name
                    }).Where(w => w.Code.Contains(keywordForSearch) || w.Name.Contains(keywordForSearch) || w.ProductCategorieCode.Contains(keywordForSearch) || w.ProductGroupCode.Contains(keywordForSearch) || w.ProductCategorieName.Contains(keywordForSearch) || w.ProductGroupName.Contains(keywordForSearch)).GroupBy(g => g.Id).Select(s => s.First()).OrderBy(o => o.Id);

            //return HuahuiDbContext.Product.Where(w => w.Code.Contains(keywordForSearch) || w.Name.Contains(keywordForSearch) || w.ProductCategorieCode.Contains(keywordForSearch) || w.ProductGroupCode.Contains(keywordForSearch)).AsNoTracking().OrderBy(o => o.Id);
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
