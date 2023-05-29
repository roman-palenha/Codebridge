using Codebridge.DataLayer.Entities;

namespace Codebridge.Business.Interfaces
{
    public interface ISortingSpecification<T>
    {
        IQueryable<T> ApplySorting(IQueryable<Dog> queryable);
    }
}
