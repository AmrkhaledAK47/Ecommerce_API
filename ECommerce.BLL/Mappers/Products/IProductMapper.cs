using ECommerce.DAL;

namespace ECommerce.BLL
{
    public interface IProductMapper
    {
        IEnumerable<ProductReadAllDTO> MapToProductReadAllDTOs(IEnumerable<Product> products);
        ProductReadDetailsDTO MapToProductReadDetailsDTO(Product product);
        ProductEditDTO MapToProductEditDTO(Product product);
        Product MapToProductEntity(ProductCreateDTO product);
        void MapToExistingProductEntity(ProductEditDTO product, Product entity);
    }
}