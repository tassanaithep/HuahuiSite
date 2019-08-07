using HuahuiSite.Core.Entities;
using HuahuiSite.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces.Repositories
{
    public interface ICartItemListRepository : IRepository<CartItemList>
    {
        IEnumerable<CartItemListModel> GetCartItemList();
        IEnumerable<CartItemListModel> GetCartItemListByUser(int userId);
        IEnumerable<CartItemList> GetCartItemListByCard(int cardId);
        CartItemListModel GetCartItemListByCardAndProduct(int cardId, int productId);
    }
}
