using ProductManageAPI.DTO;

namespace ProductManageAPI.ProviderInterface
{
    public interface IProductProvider
    {
        Task<AllProductResponseDTO> GetAllProducts();
        Task<AllProductResponseDTO> GetProductsById(ProductRequestDTO productRequestDTO);
        Task<BaseResponseDTO> UpdateProductById(ProductRequestDTO productRequestDTO);
        Task<BaseResponseDTO> DeleteProductById(ProductRequestDTO productRequestDTO);
        Task<BaseResponseDTO> DecrementStock(DecrementProductRequestDTO decrementProductRequestDTO);
        Task<BaseResponseDTO> AddToStock(DecrementProductRequestDTO decrementProductRequestDTO);
        Task<BaseResponseDTO> CreateProduct(ProductDTO productDTO);
    }
}
