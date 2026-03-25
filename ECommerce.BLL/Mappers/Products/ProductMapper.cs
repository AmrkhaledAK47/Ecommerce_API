using ECommerce.DAL;

namespace ECommerce.BLL
{
    public class ProductMapper : IProductMapper
    {
        public IEnumerable<ProductReadAllDTO> MapToProductReadAllDTOs(IEnumerable<Product> products)
        {
            return products.Select(p => new ProductReadAllDTO
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                ImageUrl = p.ImageUrl!,
                CategoryName = p.Category.Name
            }).ToList();
        }
        public ProductReadDetailsDTO MapToProductReadDetailsDTO(Product product)
        {
            return new ProductReadDetailsDTO
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                ImageUrl = product.ImageUrl!,
                Description = product.Description!,
                Count = product.Count,
                CategoryName = product.Category.Name,
                ExpiryDate = product.ExpiryDate
            };
        }
        public ProductEditDTO MapToProductEditDTO(Product product)
        {
            return new ProductEditDTO
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                CurrentImageUrl = product.ImageUrl!,
                Description = product.Description!,
                Count = product.Count,
                CategoryId = product.CategoryId,
                ExpiryDate = product.ExpiryDate
            };
        }
        public Product MapToProductEntity(ProductCreateDTO product)
        {
            return new Product
            {
                Title = product.Title,
                Price = product.Price,
                ImageUrl = product.ImageUrl!,
                Description = product.Description!,
                Count = product.Count,
                CategoryId = product.CategoryId,
                ExpiryDate = product.ExpiryDate
            };
        }
        public void MapToExistingProductEntity(ProductEditDTO product, Product entity)
        {
            entity.Title = product.Title;
            entity.Price = product.Price;
            entity.ImageUrl = product.CurrentImageUrl!;
            entity.Description = product.Description!;
            entity.Count = product.Count;
            entity.CategoryId = product.CategoryId;
            entity.ExpiryDate = product.ExpiryDate;
        }
    }
}
