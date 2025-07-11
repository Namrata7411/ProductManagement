using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ProductManageAPI.Domain;
using ProductManageAPI.DTO;
using ProductManageAPI.ProviderInterface;
using ProductManageAPI.RepositoryInterface;
using System;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace ProductManageAPI.Provider
{
    public class ProductProvider : IProductProvider
    {

        private readonly IServiceProvider _serviceProvider;
        private IMapper Mapper => _serviceProvider.GetService<IMapper>();
        public ProductProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private T GetService<T>() => _serviceProvider.GetService<T>();

        public async Task<AllProductResponseDTO> GetAllProducts()
        {
            AllProductResponseDTO responseDTO = new AllProductResponseDTO();
            try
            {
                AllProductResponseDomain ResponseDomain = new AllProductResponseDomain();
                ResponseDomain = await GetService<IProductRepository>().GetAllProducts();
                responseDTO = Mapper.Map<AllProductResponseDTO>(ResponseDomain);
            }
            catch (Exception ex)
            {
                responseDTO.IsSuccess = false;
                responseDTO.Message = ex.Message;
            }
            return responseDTO;
        }

        public async Task<AllProductResponseDTO> GetProductsById(ProductRequestDTO productRequestDTO)
        {
            AllProductResponseDTO ResponseDTO = new AllProductResponseDTO();
            try
            {
                AllProductResponseDomain ResponseDomain = new AllProductResponseDomain();
                var enityRequest = Mapper.Map<ProductRequestDomain>(productRequestDTO);
                ResponseDomain = await GetService<IProductRepository>().GetProductsById(enityRequest);
                ResponseDTO = Mapper.Map<AllProductResponseDTO>(ResponseDomain);
            }
            catch (Exception ex)
            {
                ResponseDTO.Message = ex.Message;
            }
            return ResponseDTO;
        }

        public async Task<BaseResponseDTO> UpdateProductById(ProductRequestDTO productRequestDTO)
        {
            BaseResponseDTO baseResponseDTO = new BaseResponseDTO();

            try
            {
                var existing = await GetProductsById(new ProductRequestDTO { Id = productRequestDTO.Id });

                if (existing?.products == null || !existing.products.Any())
                    return new BaseResponseDTO { IsSuccess = false, Message = "No record found for the given product ID." };

                var productToUpdate = existing.products.First();

                productToUpdate.Name = productRequestDTO.Product.Name;
                productToUpdate.Description = productRequestDTO.Product.Description;
                productToUpdate.Price = productRequestDTO.Product.Price;
                productToUpdate.Stock = productRequestDTO.Product.Stock;

                var entityRequest = Mapper.Map<ProductEntity>(productToUpdate);

                var updatedEntity = await GetService<IProductRepository>().UpdateProductById(entityRequest);

                baseResponseDTO.IsSuccess = true;
                baseResponseDTO.Message = "Product updated successfully.";
            }
            catch (Exception ex)
            {
                baseResponseDTO.IsSuccess = false;
                baseResponseDTO.Message = $"Error: {ex.Message}";
            }

            return baseResponseDTO;
        }

        public async Task<BaseResponseDTO> DeleteProductById(ProductRequestDTO productRequestDTO)
        {
            BaseResponseDTO ResponseDTO = new BaseResponseDTO();
            try
            {
                BaseResponseEntity baseResponseEntity = new BaseResponseEntity();
                var enityRequest = Mapper.Map<ProductRequestDomain>(productRequestDTO);
                baseResponseEntity = await GetService<IProductRepository>().DeleteProductById(enityRequest);
                ResponseDTO = Mapper.Map<BaseResponseDTO>(baseResponseEntity);
            }
            catch (Exception ex)
            {
                ResponseDTO.Message = ex.Message;
            }
            return ResponseDTO;
        }

        public async Task<BaseResponseDTO> DecrementStock(DecrementProductRequestDTO decrementProductRequestDTO)
        {
            BaseResponseDTO ResponseDTO = new BaseResponseDTO();
            try
            {
                BaseResponseEntity baseResponseEntity = new BaseResponseEntity();
                var enityRequest = Mapper.Map<DecrementProductRequestDomain>(decrementProductRequestDTO);
                baseResponseEntity = await GetService<IProductRepository>().DecrementStock(enityRequest);
                ResponseDTO = Mapper.Map<BaseResponseDTO>(baseResponseEntity);
            }
            catch (Exception ex)
            {
                ResponseDTO.Message = ex.Message;
                ResponseDTO.IsSuccess = false;
            }
            return ResponseDTO;
        }

        public async Task<BaseResponseDTO> AddToStock(DecrementProductRequestDTO decrementProductRequestDTO)
        {
            BaseResponseDTO ResponseDTO = new BaseResponseDTO();
            try
            {
                BaseResponseEntity baseResponseEntity = new BaseResponseEntity();
                var enityRequest = Mapper.Map<DecrementProductRequestDomain>(decrementProductRequestDTO);
                baseResponseEntity = await GetService<IProductRepository>().AddToStock(enityRequest);
                ResponseDTO = Mapper.Map<BaseResponseDTO>(baseResponseEntity);
            }
            catch (Exception ex)
            { 
                ResponseDTO.Message = ex.Message;
                ResponseDTO.IsSuccess = false;
            }
            return ResponseDTO;
        }
        public async Task<BaseResponseDTO> CreateProduct(ProductDTO productDTO)
        {
            var response = new BaseResponseDTO();

            try
            {
                var productRepository = GetService<IProductRepository>();

                const int maxAttempts = 5;
                int attempts = 0;
                bool success = false;
                string generatedId = "";

                while (!success && attempts < maxAttempts)
                {
                    generatedId = new Random().Next(100000, 999999).ToString();

                    if (!await productRepository.ExistsAsync(generatedId))
                    {
                        success = true;
                    }
                    else
                    {
                        attempts++;
                    }
                }

                if (!success)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to generate a unique product ID. Please try again.";
                    return response;
                }

                productDTO.ProductId = generatedId;
                var productEntity = Mapper.Map<ProductEntity>(productDTO);

                await productRepository.CreateProduct(productEntity);

                response.IsSuccess = true;
                response.Message = "Product created successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
