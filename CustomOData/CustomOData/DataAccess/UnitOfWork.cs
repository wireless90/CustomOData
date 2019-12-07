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

        public IUnitOfWork Begin()

        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }
            _dbTransaction = _dbConnection.BeginTransaction();

            return this;
        }

        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
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
            

            if (_dbTransaction != null)
            {
                _dbTransaction.Dispose();
            }

            if (_dbConnection != null)
            {
                Close();
                _dbConnection.Dispose();
            }


            _dbTransaction = null;
            _dbConnection = null;
        }

        public IDbTransaction GetDbTransaction()
        {
            return _dbTransaction;
        }

        public IDbConnection GetDbConnection()
        {
            return _dbConnection;
        }

        private IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
    }
}