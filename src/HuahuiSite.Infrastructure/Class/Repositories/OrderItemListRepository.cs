using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Interfaces.Repositories;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuahuiSite.Infrastructure.Class.Repositories
{
    public class OrderItemListRepository : Repository<OrderItemList>, IOrderItemListRepository
    {
        public OrderItemListRepository(HuahuiDbContext context) : base(context)
        {
        }

        public HuahuiDbContext HuahuiDbContext
        {
            get { return Context as HuahuiDbContext; }
        }

        public IEnumerable<OrderItemList> GetOrderItemListByOrder(int orderId)
        {
            return HuahuiDbContext.OrderItemList.Where(w => w.OrderId.Equals(orderId));
        }

        public IEnumerable<OrderItemListModel> GetOrderItemList()
        {
            return (from orderItemList in HuahuiDbContext.OrderItemList
                    join productJoin in HuahuiDbContext.Product on orderItemList.ProductId equals productJoin.Id into OrderItemListJoinProduct
                    from product in OrderItemListJoinProduct.DefaultIfEmpty()
                    join productGroupJoin in HuahuiDbContext.ProductGroup on product.ProductGroupCode equals productGroupJoin.Code into ProductJoinProductGroup
                    from productGroup in ProductJoinProductGroup.DefaultIfEmpty()
                    select new OrderItemListModel
                    {
                        Id = orderItemList.Id,
                        OrderId = orderItemList.OrderId,
                        ProductId = orderItemList.ProductId,
                        Quantity = orderItemList.Quantity,
                        TotalPrice = orderItemList.TotalPrice,
                        CreatedDateTime = orderItemList.CreatedDateTime,
                        ProductName = product.Name,
                        UnitPrice = productGroup.UnitPrice,
                        PictureFileName = product.PictureFileName
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<OrderItemListModel> GetCompleteOrderItemList()
        {
            return (from orderItemList in HuahuiDbContext.OrderItemList
                    join orderJoin in HuahuiDbContext.Order.Where(w => w.Status.Equals("Approve")) on orderItemList.OrderId equals orderJoin.Id into OrderItemListJoinOrder
                    from order in OrderItemListJoinOrder.DefaultIfEmpty()
                    join productJoin in HuahuiDbContext.Product on orderItemList.ProductId equals productJoin.Id into CartItemListJoinProduct
                    from product in CartItemListJoinProduct.DefaultIfEmpty()
                    join productGroupJoin in HuahuiDbContext.ProductGroup on product.ProductGroupCode equals productGroupJoin.Code into ProductJoinProductGroup
                    from productGroup in ProductJoinProductGroup.DefaultIfEmpty()
                    select new OrderItemListModel
                    {
                        Id = orderItemList.Id,
                        OrderId = orderItemList.OrderId,
                        ProductId = orderItemList.ProductId,
                        Quantity = orderItemList.Quantity,
                        TotalPrice = orderItemList.TotalPrice,
                        CreatedDateTime = orderItemList.CreatedDateTime,
                        ProductName = product.Name,
                        UnitPrice = productGroup.UnitPrice,
                        PictureFileName = product.PictureFileName
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }

        public IEnumerable<OrderItemListModel> GetNotCompleteOrderItemList()
        {
            return (from orderItemList in HuahuiDbContext.OrderItemList
                    join orderJoin in HuahuiDbContext.Order.Where(w => w.Status.Equals("Confirm") || w.Status.Equals("Approve")) on orderItemList.OrderId equals orderJoin.Id into OrderItemListJoinOrder
                    from order in OrderItemListJoinOrder.DefaultIfEmpty()
                    join productJoin in HuahuiDbContext.Product on orderItemList.ProductId equals productJoin.Id into CartItemListJoinProduct
                    from product in CartItemListJoinProduct.DefaultIfEmpty()
                    join productGroupJoin in HuahuiDbContext.ProductGroup on product.ProductGroupCode equals productGroupJoin.Code into ProductJoinProductGroup
                    from productGroup in ProductJoinProductGroup.DefaultIfEmpty()
                    select new OrderItemListModel
                    {
                        Id = orderItemList.Id,
                        OrderId = orderItemList.OrderId,
                        ProductId = orderItemList.ProductId,
                        Quantity = orderItemList.Quantity,
                        TotalPrice = orderItemList.TotalPrice,
                        CreatedDateTime = orderItemList.CreatedDateTime,
                        ProductName = product.Name,
                        UnitPrice = productGroup.UnitPrice,
                        PictureFileName = product.PictureFileName
                    }).GroupBy(g => g.Id).Select(s => s.First()).ToList();
        }
    }
}
