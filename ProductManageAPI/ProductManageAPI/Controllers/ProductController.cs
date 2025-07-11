using Microsoft.AspNetCore.Mvc;
using ProductManageAPI.DTO;
using ProductManageAPI.ProviderInterface;

namespace ProductManageAPI.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public ProductController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        private T GetService<T>() => _serviceProvider.GetService<T>();

        [Route("/GetAllProducts")]
        [HttpGet]
        public async Task<AllProductResponseDTO> GetAllProducts()
        {
           return await GetService<IProductProvider>().GetAllProducts();
        }

        [Route("/GetProductsById/{id}")]
        [HttpGet]
        public async Task<AllProductResponseDTO> GetProductsById(ProductRequestDTO productRequestDTO)
        {
          return await GetService<IProductProvider>().GetProductsById(productRequestDTO);
        }

        [Route("/UpdateProductById/{id}")]
        [HttpPut]
        public async Task<BaseResponseDTO> UpdateProductById(ProductRequestDTO productRequestDTO)
        {
          return  await GetService<IProductProvider>().UpdateProductById(productRequestDTO);
        }

        [Route("/DeleteProductById/{id}")]
        [HttpDelete]
        public async Task<BaseResponseDTO> DeleteProductById(ProductRequestDTO productRequestDTO)
        {
          return await GetService<IProductProvider>().DeleteProductById(productRequestDTO);
        }

        [Route("decrement-stock/{id}/{quantity}")]
        [HttpPut]
        public async Task<BaseResponseDTO> DecrementStock(DecrementProductRequestDTO decrementProductRequestDTO)
        {
           return await GetService<IProductProvider>().DecrementStock(decrementProductRequestDTO);
        }

        [Route("add-to-stock/{id}/{quantity}")]
        [HttpPut]
        public async Task<BaseResponseDTO> AddToStock(DecrementProductRequestDTO decrementProductRequestDTO)
        {
          return await GetService<IProductProvider>().AddToStock(decrementProductRequestDTO);
        }
        [Route("api/products")]
        [HttpPost]
        public async Task<BaseResponseDTO> CreateProduct([FromBody] ProductDTO productDTO)
        {
            return await GetService<IProductProvider>().CreateProduct(productDTO);
        }
    }
}
