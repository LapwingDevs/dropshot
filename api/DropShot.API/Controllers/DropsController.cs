using DropShot.Application.Interfaces;
using DropShot.Application.Models.Drops;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DropsController : ControllerBase
{
    private readonly IDropsService _dropsService;

    public DropsController(IDropsService dropsService)
    {
        _dropsService = dropsService;
    }
    
    [HttpPost]
    public async Task AddDrop(AddDropRequest request)
    {
        await _dropsService.AddDrop(request);
    }
    
    [HttpGet]
    public async Task<DropsVm> GetDrops()
    {
        return new DropsVm();
    }
}