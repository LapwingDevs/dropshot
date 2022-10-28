using DropShot.Application.Products.Models;

namespace DropShot.Application.Products.Interfaces;

public interface IVariantsService
{
    Task<IEnumerable<VariantDto>> GetAllVariantsInWarehouse();
    Task AddVariantToProduct(AddVariantToProductRequest request);
}