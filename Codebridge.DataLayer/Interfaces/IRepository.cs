namespace Codebridge.DataLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task AddAsync(T entity);
        Task SaveAsync();
    }
}
