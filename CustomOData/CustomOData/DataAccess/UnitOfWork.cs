using CustomOData.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CustomOData.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Begin()
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            _dbTransaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
            Dispose();
        }

        public void Close()
        {
            if (_dbConnection.State != ConnectionState.Closed)
            {
                _dbConnection.Close();
            }
        }
        public void Dispose()
        {
            Close();

            if (_dbTransaction != null)
            {
                _dbTransaction.Dispose();
            }

            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
            }


            _dbTransaction = null;
            _dbConnection = null;
        }

        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
    }
}