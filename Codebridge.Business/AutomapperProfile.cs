using AutoMapper;
using Codebridge.Business.Dtos;
using Codebridge.DataLayer.Entities;

namespace Codebridge.Business
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DogDto, Dog>();
        }
    }
}
