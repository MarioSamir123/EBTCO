namespace EBTCO.Core.Contract.DBRepo
{
    public interface IBaseRepo<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetSource();
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
    }
}