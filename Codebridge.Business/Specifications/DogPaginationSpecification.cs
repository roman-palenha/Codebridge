using Codebridge.Business.Interfaces;
using Codebridge.DataLayer.Entities;

namespace Codebridge.Business.Specifications
{
    public class DogPaginationSpecification : IPaginationSpecification<Dog>
    {
        private readonly int _pageNumber;
        private readonly int _pageSize;

        public DogPaginationSpecification(int pageNumber, int pageSize)
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
        }

        public IQueryable<Dog> ApplyPagination(IQueryable<Dog> queryable)
        {
            var skip = (_pageNumber - 1) * _pageSize;
            return queryable.Skip(skip).Take(_pageSize);
        }
    }
}
