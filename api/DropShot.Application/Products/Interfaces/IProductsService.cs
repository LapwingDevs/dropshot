using DropShot.Application.Products.Models;

namespace DropShot.Application.Products.Interfaces;

public interface IProductsService
{
    Task<IEnumerable<ProductOnListDto>> GetProducts();
    Task<ProductDetailsDto> GetProductById(int productId);
    
    Task AddProduct(AddProductRequest request);
}