using EBTCO.Core.Contract.DBRepo;
using EBTCO.DB;

namespace EBTCO.RDS.Implementation
{
    public class BaseRepository<T> : IBaseRepo<T> where T : class
    {
        protected readonly AppDbContext _appDBContext;

        public BaseRepository(AppDbContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _appDBContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _appDBContext.Set<T>().Update(entity);
            return entity;
        }

        public IQueryable<T> GetSource()
        {

            IQueryable<T> source = _appDBContext.Set<T>();
            return source;
        }
    }
}
