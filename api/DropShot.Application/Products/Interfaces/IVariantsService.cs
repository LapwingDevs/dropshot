using DropShot.Application.Products.Models;

namespace DropShot.Application.Products.Interfaces;

public interface IVariantsService
{
    Task AddVariantToProduct(AddVariantToProductRequest request);
}