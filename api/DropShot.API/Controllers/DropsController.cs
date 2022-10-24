using DropShot.Application.Drops;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Drops.Models;
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

    [HttpGet("dropId")]
    public async Task<DropDetailsDto> GetDropDetails(int dropId)
    {
        return await _dropsService.GetDropDetails(dropId);
    }

    [HttpGet]
    public async Task<DropsLandingPageVm> GetDrops()
    {
        return await _dropsService.GetDrops();
    }

    // ADMIN PANEL ENDPOINT
    [HttpGet("admin")]
    public async Task<IEnumerable<DropDetailsDto>> GetDropsWithDetails()
    {
        return await _dropsService.GetDropsWithDetails();
    }


    [HttpPost]
    public async Task AddDrop(AddDropRequest request)
    {
        // await _dropsService.AddDrop(request);
    }
}