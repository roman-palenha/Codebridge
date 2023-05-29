using Codebridge.Business.Interfaces;
using Codebridge.DataLayer.Entities;
using System.Linq.Expressions;

namespace Codebridge.Business.Specifications
{
    public class DogSortingSpecification : ISortingSpecification<Dog>
    {
        private readonly string _attribute;
        private readonly bool _isDescending;

        public DogSortingSpecification(string? attribute, string? order)
        {
            _attribute = attribute ?? "name";
            _isDescending = order?.ToLower() == "desc";
        }

        public IQueryable<Dog> ApplySorting(IQueryable<Dog> queryable)
        {
            return _isDescending ? queryable.OrderByDescending(GetSortingExpression(_attribute)) : queryable.OrderBy(GetSortingExpression(_attribute));
        }

        private Expression<Func<Dog, object>> GetSortingExpression(string attribute)
        {
            return attribute switch
            {
                "weight" => dog => dog.Weight,
                "color" => dog => dog.Color,
                "name" => dog => dog.Name,
                "tail_length" => dog => dog.TailLength,
                _ => dog => dog.Name
            };
        }
    }
}
