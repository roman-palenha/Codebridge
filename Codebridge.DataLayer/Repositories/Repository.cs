using Codebridge.DataLayer.Interfaces;

namespace Codebridge.DataLayer.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext DbContext;

        protected Repository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public  IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
