namespace Codebridge.Business.Interfaces
{
    public interface IPaginationSpecification<T>
    {
        IQueryable<T> ApplyPagination(IQueryable<T> queryable);
    }
}
