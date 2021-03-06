﻿using System;
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

        public IEnumerable<CartItemListModel> GetCartItemList()
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
                        TotalPrice = cartItemList.TotalPrice,
                        CreatedDateTime = cartItemList.CreatedDateTime,
                        ProductName = product.Name,
                        ProductGroupCode = product.ProductGroupCode,
                        IsPromotion = product.IsPromotion,
                        PictureFileName = product.PictureFileName,
                        UnitPrice = productGroup.UnitPrice
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<CartItemListModel> GetCartItemListByUser(int userId)
        {
            return (from cartItemList in HuahuiDbContext.CartItemList
                    join cartJoin in HuahuiDbContext.Cart.Where(w => w.UserId.Equals(userId) && w.IsActive.Equals(true)) on cartItemList.CardId equals cartJoin.Id into CartItemListJoinCart
                    from cart in CartItemListJoinCart
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
                        TotalPrice = cartItemList.TotalPrice,
                        CreatedDateTime = cartItemList.CreatedDateTime,
                        ProductName = product.Name,
                        ProductGroupCode = product.ProductGroupCode,
                        UnitPrice = productGroup.UnitPrice,
                        PromotionPrice = productGroup.PromotionPrice.Value,
                        MinQuantity = productGroup.MinQuantity,
                        MaxQuantity = productGroup.MaxQuantity,
                        IsPromotion = product.IsPromotion,
                        PictureFileName = product.PictureFileName
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<CartItemListModel> GetCartItemListNotApproveByUser(int userId)
        {
            return (from cartItemList in HuahuiDbContext.CartItemList
                    join cartJoin in HuahuiDbContext.Cart.Where(w => w.UserId.Equals(userId) && w.IsActive.Equals(true)) on cartItemList.CardId equals cartJoin.Id into CartItemListJoinCart
                    from cart in CartItemListJoinCart
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
                        TotalPrice = cartItemList.TotalPrice,
                        CreatedDateTime = cartItemList.CreatedDateTime,
                        ProductName = product.Name,
                        UnitPrice = productGroup.UnitPrice,
                        MinQuantity = productGroup.MinQuantity,
                        MaxQuantity = productGroup.MaxQuantity,
                        PictureFileName = product.PictureFileName
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<CartItemList> GetCartItemListByCardId(int cardId)
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
                CreatedDateTime = s.CreatedDateTime,
                UpdatedDateTime = s.UpdatedDateTime
            }).FirstOrDefault();
        }

        public IEnumerable<CartItemListModel> GetCartItemListOfConfirmCart()
        {
            return (from cartItemList in HuahuiDbContext.CartItemList
                    join cartJoin in HuahuiDbContext.Cart.Where(w => w.Status.Equals("Confirm")) on cartItemList.CardId equals cartJoin.Id into CartItemListJoinCart
                    from cart in CartItemListJoinCart.DefaultIfEmpty()
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
                        TotalPrice = cartItemList.TotalPrice,
                        CreatedDateTime = cartItemList.CreatedDateTime,
                        ProductName = product.Name,
                        PictureFileName = product.PictureFileName,
                        ProductGroupCode = product.ProductGroupCode,
                        IsPromotion = product.IsPromotion,
                        UnitPrice = productGroup.UnitPrice,
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<CartItemListModel> GetCompleteOrderItemListData()
        {
            return (from cartItemList in HuahuiDbContext.CartItemList
                    join cartJoin in HuahuiDbContext.Cart.Where(w => w.Status.Equals("Approve")) on cartItemList.CardId equals cartJoin.Id into CartItemListJoinCart
                    from cart in CartItemListJoinCart.DefaultIfEmpty()
                    join productJoin in HuahuiDbContext.Product on cartItemList.ProductId equals productJoin.Id into CartItemListJoinProduct
                    from product in CartItemListJoinProduct.DefaultIfEmpty()
                    join productGroupJoin in HuahuiDbContext.ProductGroup on product.ProductGroupCode equals productGroupJoin.Code into ProductJoinProductGroup
                    from productGroup in ProductJoinProductGroup.DefaultIfEmpty()
                    select new CartItemListModel
                    {
                        Id = cartItemList.Id,
                        OrderId = cart.OrderId,
                        ProductId = cartItemList.ProductId,
                        Quantity = cartItemList.Quantity,
                        TotalPrice = cartItemList.TotalPrice,
                        CreatedDateTime = cartItemList.CreatedDateTime,
                        ProductName = product.Name,
                        UnitPrice = productGroup.UnitPrice,
                        PictureFileName = product.PictureFileName
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }
    }
}
