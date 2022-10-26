using DropShot.Application.Products.Interfaces;
using DropShot.Application.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductOnListDto>> GetProducts()
    {
        return await _productsService.GetProducts();
    }

    [HttpPost]
    public async Task AddProduct(AddProductRequest request)
    {
        await _productsService.AddProduct(request);
    }
}