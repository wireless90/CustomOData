using System;
using System.Data;

namespace CustomOData.DataAccess.Abstractions
{
    public interface IUnitOfWork:IDisposable
    {
        IUnitOfWork Begin();
        void Close();
        void Commit();
        void Rollback();

        IDbTransaction GetDbTransaction();
        IDbConnection GetDbConnection();
    }
}