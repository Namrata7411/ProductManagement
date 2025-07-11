using ProductManageAPI.Domain;

namespace ProductManageAPI.RepositoryInterface
{
    public interface IProductRepository
    {
        Task<AllProductResponseDomain> GetAllProducts();
        Task<AllProductResponseDomain> GetProductsById(ProductRequestDomain productDomain);
        Task<BaseResponseEntity> UpdateProductById(ProductEntity product);
        Task<BaseResponseEntity> DeleteProductById(ProductRequestDomain productRequestDomain);
        Task<BaseResponseEntity> DecrementStock(DecrementProductRequestDomain decrementProductRequestDomain);
        Task<BaseResponseEntity> AddToStock(DecrementProductRequestDomain decrementProductRequestDomain);
        Task<bool> ExistsAsync(string productId);
        Task CreateProduct(ProductEntity product);
    }
}
