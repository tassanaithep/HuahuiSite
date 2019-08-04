using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using HuahuiSite.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class CartItemListRepository : Repository<CartItemList>, ICartItemListRepository
    {
        public CartItemListRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }

        public IEnumerable<CartItemListModel> GetCartItemListByUser(int userId)
        {
            return (from cartItemList in HuahuiDbContext.CartItemList
                    join productJoin in HuahuiDbContext.Product on cartItemList.ProductId equals productJoin.Id into CartItemListJoinProduct
                    from product in CartItemListJoinProduct.DefaultIfEmpty()
                    join productGroupJoin in HuahuiDbContext.ProductGroup on product.ProductGroupCode equals productGroupJoin.Code into ProductJoinProductGroup
                    from productGroup in ProductJoinProductGroup.DefaultIfEmpty()
                    select new CartItemListModel
                    {
                        Id = cartItemList.Id,
                        CardId = cartItemList.CardId,
                        ProductId = cartItemList.ProductId,
                        Quantity = cartItemList.Quantity,
                        IsActive = cartItemList.IsActive,
                        CreatedDateTime = cartItemList.CreatedDateTime,
                        ProductName = product.Name,
                        UnitPrice = productGroup.UnitPrice,
                        PictureFileName = product.PictureFileName
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<CartItemList> GetCartItemListByCard(int cardId)
        {
            return HuahuiDbContext.CartItemList.Where(w => w.CardId.Equals(cardId)).ToList();
        }

        public CartItemListModel GetCartItemListByCardAndProduct(int cardId, int productId)
        {
            return HuahuiDbContext.CartItemList.Where(w => w.CardId.Equals(cardId) && w.ProductId.Equals(productId)).Select(s => new CartItemListModel
            {
                Id = s.Id,
                CardId = s.CardId,
                ProductId = s.ProductId,
                Quantity = s.Quantity,
                TotalPrice = s.TotalPrice,
                IsActive = s.IsActive,
                CreatedDateTime = s.CreatedDateTime,
                UpdatedDateTime = s.UpdatedDateTime
            }).FirstOrDefault();
        }
    }
}
