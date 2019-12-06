using CustomOData.DataAccess.Abstractions;

namespace CustomOData.DataAccess
{
    public class DBService : IDBService
    {
        public DBService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }


    }
}