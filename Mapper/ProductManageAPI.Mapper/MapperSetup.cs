using AutoMapper;
using ProductManageAPI.Domain;
using ProductManageAPI.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductApi.Mappings
{
    public class MapperSetup : Profile
    {
        public MapperSetup()
        {
            CreateMap<ProductEntity, ProductDTO>();
        }
    }
}
