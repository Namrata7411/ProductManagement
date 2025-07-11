namespace ProductManageAPI.DTO
{

    public class BaseResponseDTO
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class AllProductResponseDTO : BaseResponseDTO
    {
        public List<ProductDTO> products { get; set; }
    }

    public class ProductDTO
    {
        public string ProductId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ErrorMsg { get; set; }
    }

    public class ProductRequestDTO 
    {
        public string Id { get; set; }
        public ProductDTO Product { get; set; }

    }


    public class CommonBaseResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }
        public string Message { get; set; }
    }

    public class DecrementProductRequestDTO : BaseResponseDTO
    {
        public string Id { get; set; }

        public int quantity { get; set; }

    }
}
