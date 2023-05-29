using Codebridge.DataLayer.Entities;
using Codebridge.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codebridge.DataLayer.Repositories
{
    public class DogRepository : Repository<Dog>, IDogRepository
    {
        public DogRepository(AppDbContext dbContext)
            : base(dbContext)
        { }

        public async Task<Dog?> GetByNameAsync(string name)
        {
            return await DbContext.Dogs.FirstOrDefaultAsync(x => x.Name.Equals(name));
        }
    }
}
