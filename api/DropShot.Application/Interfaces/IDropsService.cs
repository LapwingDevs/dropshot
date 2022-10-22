﻿using DropShot.Application.Models.Drops;

namespace DropShot.Application.Interfaces;

public interface IDropsService
{
    Task<DropsVm> GetDrops();
    Task<DropDetailsDto> GetDropDetails(int dropId);
    Task AddDrop(AddDropRequest request);
}