using EBTCO.Core.Contract.DBRepo;
using EBTCO.DB;
using EBTCO.RDS.Implementation;

namespace ToursYard.RDS.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public IBaseRepo<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            Type entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
            {
                _repositories[entityType] = new BaseRepository<TEntity>(_dbContext);
            }
            return (IBaseRepo<TEntity>)_repositories[entityType];
        }

        public async Task<int> SaveChangesAsync()
        {
            int rowEffected = await _dbContext.SaveChangesAsync();
            return rowEffected;
        }

        public async Task BeginTransction()
        {
            await _dbContext.Database.BeginTransactionAsync();
        }
        public async Task RollbackTransaction()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
        public async Task CommitTransaction()
        {
            await _dbContext.Database.CommitTransactionAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
