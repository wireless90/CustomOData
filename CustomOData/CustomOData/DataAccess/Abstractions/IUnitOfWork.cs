namespace CustomOData.DataAccess.Abstractions
{
    public interface IUnitOfWork
    {
        void Begin();
        void Close();
        void Commit();
        void Dispose();
        void Rollback();
    }
}