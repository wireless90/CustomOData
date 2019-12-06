using System;

namespace CustomOData.DataAccess.Abstractions
{
    public interface IDBService:IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}