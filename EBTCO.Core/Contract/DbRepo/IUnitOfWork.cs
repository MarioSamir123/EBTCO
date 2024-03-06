namespace EBTCO.Core.Contract.DBRepo
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepo<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();

        Task BeginTransction();
        Task RollbackTransaction();
        Task CommitTransaction();
    }
}
