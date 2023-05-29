using AutoMapper;
using Codebridge.Business.Dtos;
using Codebridge.Business.Interfaces;
using Codebridge.Business.Services;
using Codebridge.Business.Validation;
using Codebridge.DataLayer.Entities;
using Codebridge.DataLayer.Interfaces;
using MockQueryable.Moq;
using Moq;

namespace Codebridge.Tests
{
    [TestClass]
    public class DogServiceTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private DogService _dogService;

        [TestInitialize]
        public void Initialize()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _mapperMock = new Mock<IMapper>();
            _dogService = new DogService(_dogRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task GetDogsAsync_Should_ReturnDogs_When_SortingAndPaginationSpecificationsProvided()
        {
            var sortingSpecificationMock = new Mock<ISortingSpecification<Dog>>();
            var paginationSpecificationMock = new Mock<IPaginationSpecification<Dog>>();
            var dogs = (new List<Dog>
            {
                new Dog { Name = "Max", Color = "Brown", TailLength = 10, Weight = 20 },
                new Dog { Name = "Buddy", Color = "Black", TailLength = 8, Weight = 15 }
            })
                .AsQueryable()
                .BuildMock();
            _dogRepositoryMock.Setup(repo => repo.GetAll()).Returns(dogs);
            sortingSpecificationMock.Setup(spec => spec.ApplySorting(It.IsAny<IQueryable<Dog>>())).Returns((IQueryable<Dog> q) => q.OrderBy(d => d.Name));
            paginationSpecificationMock.Setup(spec => spec.ApplyPagination(It.IsAny<IQueryable<Dog>>())).Returns((IQueryable<Dog> q) => q.Take(1));

            var result = await _dogService.GetDogsAsync(sortingSpecificationMock.Object, paginationSpecificationMock.Object);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Max", result.First().Name);
        }

        [TestMethod]
        public async Task GetDogsAsync_Should_ReturnAllDogs_When_NoSortingOrPaginationSpecificationsProvided()
        {
            var dogs = (new List<Dog>
                {
                    new Dog { Name = "Max", Color = "Brown", TailLength = 10, Weight = 20 },
                    new Dog { Name = "Buddy", Color = "Black", TailLength = 8, Weight = 15 }
                })
                .AsQueryable()
                .BuildMock();
            _dogRepositoryMock.Setup(repo => repo.GetAll()).Returns(dogs.AsQueryable());

            var result = await _dogService.GetDogsAsync(null, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task AddAsync_Should_ThrowException_When_DtoNameIsNullOrWhitespace()
        {
            var dto = new DogDto { Name = "   ", Color = "Brown", TailLength = 10, Weight = 20 };

            await Assert.ThrowsExceptionAsync<CodebridgeException>(async () => await _dogService.AddAsync(dto));
        }

        [TestMethod]
        public async Task AddAsync_Should_ThrowException_When_DtoColorIsNullOrWhitespace()
        {
            var dto = new DogDto { Name = "Max", Color = "   ", TailLength = 10, Weight = 20 };

            await Assert.ThrowsExceptionAsync<CodebridgeException>(async () => await _dogService.AddAsync(dto));
        }

     

        [TestMethod]
        public async Task AddAsync_Should_ThrowException_When_DtoTailLengthIsZero()
        {
            var dto = new DogDto { Name = "Max", Color = "Brown", TailLength = 0, Weight = 20 };

            await Assert.ThrowsExceptionAsync<CodebridgeException>(async () => await _dogService.AddAsync(dto));
        }

        [TestMethod]
        public async Task AddAsync_Should_ThrowException_When_DogWithSameNameExists()
        {
            var dto = new DogDto { Name = "Max", Color = "Brown", TailLength = 10, Weight = 20 };
            var existingDog = new Dog { Name = "Max", Color = "Black", TailLength = 12, Weight = 18 };

            _dogRepositoryMock.Setup(repo => repo.GetByNameAsync(dto.Name)).ReturnsAsync(existingDog);

            await Assert.ThrowsExceptionAsync<CodebridgeException>(async () => await _dogService.AddAsync(dto));
        }

    }
}