using HuahuiSite.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository Carts { get; }
        ICartItemListRepository CartItemLists { get; }
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IProductCategorieRepository ProductCategories { get; }
        IProductGroupRepository ProductGroups { get; }
        ISaleRepository Sales { get; }
        IUnitOfProductRepository UnitOfProducts { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
