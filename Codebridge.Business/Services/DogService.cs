using AutoMapper;
using Codebridge.Business.Dtos;
using Codebridge.Business.Interfaces;
using Codebridge.Business.Validation;
using Codebridge.DataLayer.Entities;
using Codebridge.DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codebridge.Business.Services
{
    public class DogService : IDogService
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;

        public DogService(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Dog>> GetDogsAsync(ISortingSpecification<Dog>? sortingSpecification,
            IPaginationSpecification<Dog>? paginationSpecification)
        {
            var queryable = _dogRepository.GetAll();

            if (paginationSpecification != null)
                queryable = paginationSpecification.ApplyPagination(queryable);

            if (sortingSpecification != null)
                queryable = sortingSpecification.ApplySorting(queryable);

            return await queryable.ToListAsync();
        }

        public async Task AddAsync(DogDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new CodebridgeException("Name is not valid.");

            if (string.IsNullOrWhiteSpace(dto.Color))
                throw new CodebridgeException("Color is not valid.");

            if (dto.TailLength <= 0)
                throw new CodebridgeException("Tail length is negative or zero.");

            if (dto.Weight <= 0)
                throw new CodebridgeException("Weight length is negative or zero.");

            var dog = await _dogRepository.GetByNameAsync(dto.Name);
            if (dog != null)
                throw new CodebridgeException("Dog with the same name is already exists.");

            dog =_mapper.Map<Dog>(dto);
            if (dog == null)
                throw new CodebridgeException("Error while mapping dto to entity.");

            await _dogRepository.AddAsync(dog);
            await _dogRepository.SaveAsync();
        }
    }
}
