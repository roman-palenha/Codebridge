using Codebridge.Business.Dtos;
using Codebridge.Business.Interfaces;
using Codebridge.Business.Specifications;
using Codebridge.DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Codebridge.Controllers
{
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogService _dogService;

        public DogsController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet("dogs")]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDogs([FromQuery] string? attribute, [FromQuery] string? order, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            ISortingSpecification<Dog>? sortingSpecification = null;
            if (!string.IsNullOrEmpty(order))
            { 
                sortingSpecification = new DogSortingSpecification(attribute, order);
            }

            IPaginationSpecification<Dog>? paginationSpecification = null;
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                paginationSpecification = new DogPaginationSpecification(pageNumber.Value, pageSize.Value);
            }

            var dogs = await _dogService.GetDogsAsync(sortingSpecification, paginationSpecification);
            return Ok(dogs);
        }

        [HttpPost("dog")]
        public async Task<ActionResult<IEnumerable<Dog>>> CreateDog([FromBody] DogDto dto)
        {
            await _dogService.AddAsync(dto);
            return CreatedAtAction(nameof(CreateDog), dto);
        }
    }
}
