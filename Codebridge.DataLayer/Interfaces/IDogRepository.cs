using Codebridge.DataLayer.Entities;

namespace Codebridge.DataLayer.Interfaces
{
    public interface IDogRepository : IRepository<Dog>
    {
        Task<Dog?> GetByNameAsync(string name);
    }
}
