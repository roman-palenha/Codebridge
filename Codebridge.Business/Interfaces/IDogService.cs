using Codebridge.Business.Dtos;
using Codebridge.DataLayer.Entities;

namespace Codebridge.Business.Interfaces
{
    public interface IDogService
    {
        Task<IEnumerable<Dog>> GetDogsAsync(ISortingSpecification<Dog>? sortingSpecification, IPaginationSpecification<Dog>? paginationSpecification);
        Task AddAsync(DogDto dto);
    }
}
