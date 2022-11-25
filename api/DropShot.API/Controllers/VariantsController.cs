using DropShot.Application.Products.Interfaces;
using DropShot.Application.Products.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IEnumerable<VariantDto>> GetAllVariantsInWarehouse()
    {
        return await _variantsService.GetAllVariantsInWarehouse();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task AddVariantToProduct(AddVariantToProductRequest request)
    {
        await _variantsService.AddVariantToProduct(request);
    }

    [HttpDelete("{variantId}")]
    [Authorize(Roles = "Admin")]
    public async Task RemoveVariant(int variantId)
    {
        await _variantsService.RemoveVariant(variantId);
    }
}