using DropShot.Application.Products.Interfaces;
using DropShot.Application.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VariantsController : ControllerBase
{
    private readonly IVariantsService _variantsService;

    public VariantsController(IVariantsService variantsService)
    {
        _variantsService = variantsService;
    }

    [HttpGet("warehouse")]
    public async Task<IEnumerable<VariantDto>> GetAllVariantsInWarehouse()
    {
        return await _variantsService.GetAllVariantsInWarehouse();
    }

    [HttpPost]
    public async Task AddVariantToProduct(AddVariantToProductRequest request)
    {
        await _variantsService.AddVariantToProduct(request);
    }
}