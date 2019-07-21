using HuahuiSite.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HuahuiSite.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ISaleRepository Sales { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
