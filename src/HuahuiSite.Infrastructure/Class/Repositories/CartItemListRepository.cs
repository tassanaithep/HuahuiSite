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
            return (from cartListItem in HuahuiDbContext.CartItemList
                    join productJoin in HuahuiDbContext.Product on cartListItem.ProductId equals productJoin.Id into CartListItemJoinProduct
                    from product in CartListItemJoinProduct.DefaultIfEmpty()
                    join productGroupJoin in HuahuiDbContext.ProductGroup on product.ProductGroupCode equals productGroupJoin.Code into ProductJoinProductGroup
                    from productGroup in ProductJoinProductGroup.DefaultIfEmpty()
                    select new CartItemListModel
                    {
                        Id = cartListItem.Id,
                        CardId = cartListItem.CardId,
                        ProductId = cartListItem.ProductId,
                        IsActive = cartListItem.IsActive,
                        CreatedDateTime = cartListItem.CreatedDateTime,
                        ProductName = product.Name,
                        UnitPrice = productGroup.UnitPrice,
                        PictureFileName = product.PictureFileName
                    });
        }

        public IEnumerable<CartItemList> GetCartItemListByCard(int cardId)
        {
            return HuahuiDbContext.CartItemList.Where(w => w.CardId.Equals(cardId)).ToList();
        }
    }
}
