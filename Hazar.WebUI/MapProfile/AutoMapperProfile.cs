using AutoMapper;
using Hazar.EntityLayer.DTOs;
using Hazar.EntityLayer.Entities;

namespace Hazar.WebUI.MapProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
