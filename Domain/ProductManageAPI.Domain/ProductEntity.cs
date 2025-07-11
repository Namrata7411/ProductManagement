using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace ProductManageAPI.Domain
{

    public class BaseResponseEntity
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class AllProductResponseDomain : BaseResponseEntity
    {
        public List<ProductEntity> products { get; set; }
    }

    [Table(Name = "tblproducts")]
    public class ProductEntity
    {
        [Column, PrimaryKey, Identity]
        public string ProductId { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public decimal Price { get; set; }
        [Column]
        public int Stock { get; set; }
        [Column]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class ProductRequestDomain
    {
        public string Id { get; set; }
      
    }

    public class DecrementProductRequestDomain
    {
        public string Id { get; set; }

        public int quantity { get; set; }

    }
}
