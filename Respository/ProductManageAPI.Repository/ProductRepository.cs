using LinqToDB;
using ProductManageAPI.Domain;
using ProductManageAPI.DTO;
using ProductManageAPI.ProviderInterface;
using ProductManageAPI.Repository.DBSettings;
using ProductManageAPI.RepositoryInterface;
using System.Data;

namespace ProductManageAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        public async Task<AllProductResponseDomain> GetAllProducts()
        {
            AllProductResponseDomain response = new AllProductResponseDomain();
            try
            {
                using (var dbCon = new ProductManageDBFactory("DefaultConnection"))
                {
                    var productEntities = await dbCon.GetTable<ProductEntity>().With("NOLOCK").ToListAsync();
                    if (productEntities.Count > 0)
                    {
                        response.products = new List<ProductEntity>();
                        response.products.AddRange(productEntities);
                        response.IsSuccess = true;
                    }
                    else 
                    { 
                        response.IsSuccess = false;
                        response.Message = "No records found";
                    }
                }
            }
            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<AllProductResponseDomain> GetProductsById(ProductRequestDomain productRequestDomain)
        { 

            AllProductResponseDomain response = new AllProductResponseDomain();
            try
            {
                using (var dbCon = new ProductManageDBFactory("DefaultConnection"))
                {
                    var productEntities = await dbCon.GetTable<ProductEntity>().With("NOLOCK").Where(x => x.ProductId == productRequestDomain.Id).ToListAsync();
                    if (productEntities.Count > 0)
                    {
                        response.products = new List<ProductEntity>();
                        response.products.AddRange(productEntities);
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "No records found";
                    }
                }
            }
            catch (Exception ex) 
            { 
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponseEntity> UpdateProductById(ProductEntity product)
        {
            BaseResponseEntity baseResponseEntity = new BaseResponseEntity();
            using (var dbCon = new ProductManageDBFactory("DefaultConnection"))
            {
                int res = await dbCon.UpdateAsync(product);
                if (res > 0)
                {
                    baseResponseEntity.IsSuccess = true;
                    baseResponseEntity.Message = "Updated successfully.";
                }
                else
                {
                    baseResponseEntity.IsSuccess = false;
                    baseResponseEntity.Message = "Failed to Update.";
                }

            }
            return baseResponseEntity;
        }

        public async Task<BaseResponseEntity> DeleteProductById(ProductRequestDomain productRequestDomain)
        {
            BaseResponseEntity baseResponseEntity = new BaseResponseEntity();
            try
            {
                using (var dbCon = new ProductManageDBFactory("DefaultConnection"))
                {
                    var product = await dbCon.GetTable<ProductEntity>().With("NOLOCK").FirstOrDefaultAsync(x => x.ProductId == productRequestDomain.Id);

                    if (product != null)
                    {
                        await dbCon.DeleteAsync(product);
                        baseResponseEntity.IsSuccess = true;
                        baseResponseEntity.Message = "Product deleted successfully.";
                    }
                    baseResponseEntity.Message = "Product not found";
                }
            }
            catch(Exception ex)
            {
                baseResponseEntity.IsSuccess = false;
                baseResponseEntity.Message = ex.Message;
            }
            return baseResponseEntity;
        }

        public async Task<BaseResponseEntity> DecrementStock(DecrementProductRequestDomain decrementProductRequestDomain)
        {
            BaseResponseEntity baseResponseEntity = new BaseResponseEntity();
            try
            {
                using (var dbCon = new ProductManageDBFactory("DefaultConnection"))
                {
                    var product = await dbCon.GetTable<ProductEntity>().With("NOLOCK").Where(x => x.ProductId == decrementProductRequestDomain.Id && x.Stock >= decrementProductRequestDomain.quantity)
                                  .FirstOrDefaultAsync();

                    if (product != null)
                    {
                        product.Stock -= decrementProductRequestDomain.quantity;
                        await dbCon.UpdateAsync(product);
                        baseResponseEntity.IsSuccess = true;
                        baseResponseEntity.Message = "Stock decremented successfully.";
                    }
                    baseResponseEntity.Message = "Product not found or insufficient stock.";
                }
            }
            catch (Exception ex) 
            {
                baseResponseEntity.IsSuccess = false;
                baseResponseEntity.Message = ex.Message;
            }
            return baseResponseEntity;
        }

        public async Task<BaseResponseEntity> AddToStock(DecrementProductRequestDomain decrementProductRequestDomain)
        {
            BaseResponseEntity baseResponseEntity = new BaseResponseEntity();
            try
            {
                using (var dbCon = new ProductManageDBFactory("DefaultConnection"))
                {
                    var product = await dbCon.GetTable<ProductEntity>().With("NOLOCK").Where(x => x.ProductId == decrementProductRequestDomain.Id && x.Stock >= decrementProductRequestDomain.quantity)
                                      .FirstOrDefaultAsync();
                    if (product != null)
                    {
                        product.Stock += decrementProductRequestDomain.quantity;
                        await dbCon.UpdateAsync(product);
                        baseResponseEntity.IsSuccess = true;
                        baseResponseEntity.Message = "Stock added successfully.";
                    }
                    baseResponseEntity.Message = "Product not found";
                }
            }
            catch (Exception e)
            {
                baseResponseEntity.IsSuccess=false;
                baseResponseEntity.Message = e.Message;
            }
            return baseResponseEntity;
        }
        public async Task<bool> ExistsAsync(string productId)
        {
            using var dbCon = new ProductManageDBFactory("DefaultConnection");
            return await dbCon.GetTable<ProductEntity>().AnyAsync(p => p.ProductId == productId);
        }
        public async Task CreateProduct(ProductEntity product)
        {
            using var dbCon = new ProductManageDBFactory("DefaultConnection");
            await dbCon.InsertAsync(product);
        }

    }
}
