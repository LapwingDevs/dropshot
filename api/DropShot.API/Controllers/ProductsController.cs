using DropShot.Application.Products.Interfaces;
using DropShot.Application.Products.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IEnumerable<ProductOnListDto>> GetProducts()
    {
        return await _productsService.GetProducts();
    }
    
    [HttpGet("{productId}")]
    [Authorize]
    public async Task<ProductDetailsDto> GetProducts(int productId)
    {
        return await _productsService.GetProductById(productId);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task AddProduct(AddProductRequest request)
    {
        await _productsService.AddProduct(request);
    }
}