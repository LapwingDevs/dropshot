using DropShot.Application.Products.Models;

namespace DropShot.Application.Products.Interfaces;

public interface IProductsService
{
    Task<IEnumerable<ProductOnListDto>> GetProducts();
    
    Task AddProduct(AddProductRequest request);
}