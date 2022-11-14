using System.ComponentModel;
using DropShot.Application.Common;
using DropShot.Domain.Constants;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Infrastructure.DAL;

public class DropShotDbContextInitializer
{
    private readonly DropShotDbContext _dbContext;
    private readonly IAppDateTime _appDateTime;

    public DropShotDbContextInitializer(DropShotDbContext dbContext, IAppDateTime appDateTime)
    {
        _dbContext = dbContext;
        _appDateTime = appDateTime;
    }

    public async Task InitDatabase() => await _dbContext.Database.MigrateAsync();

    public async Task SeedDatabase()
    {
        // TODO: Seed roles, users

        // Default data
        // Seed, if necessary
        if (_dbContext.Products.Any() == false)
        {
            var products = new List<Product>()
            {
                new()
                {
                    Name = "Buty nike",
                    Description = "Buty do biegania ;]]",
                    Price = Convert.ToDecimal(213.70),
                    UnitOfSize = ClothesUnitOfMeasure.Number,
                    Variants = new List<Variant>()
                    {
                        new() { Size = 38, Status = VariantStatus.Warehouse },
                        new() { Size = 39, Status = VariantStatus.Warehouse },
                        new() { Size = 40, Status = VariantStatus.Warehouse },
                        new() { Size = 40, Status = VariantStatus.Warehouse },
                        new() { Size = 40, Status = VariantStatus.Warehouse },
                        new() { Size = 41, Status = VariantStatus.Warehouse },
                        new() { Size = 42, Status = VariantStatus.Warehouse },
                        new() { Size = 43, Status = VariantStatus.Warehouse },
                        new() { Size = 43, Status = VariantStatus.Warehouse },
                        new() { Size = 44, Status = VariantStatus.Warehouse },
                        new() { Size = 45, Status = VariantStatus.Warehouse },
                        new() { Size = 45, Status = VariantStatus.Warehouse },
                        new() { Size = 45, Status = VariantStatus.Warehouse },
                    }
                },
                new()
                {
                    Name = "Bluza adidas",
                    Description = "Taka z czterema paskami, jeden w bonusie",
                    Price = Convert.ToDecimal(200.00),
                    UnitOfSize = ClothesUnitOfMeasure.Letter,
                    Variants = new List<Variant>()
                    {
                        new() { Size = ClothesSizes.LetterSizes["S"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["S"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["M"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["M"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["M"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["L"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["L"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["XL"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["XL"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["XL"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["XXL"], Status = VariantStatus.Warehouse },
                    }
                },
                new()
                {
                    Name = "Koszulka z duzym logo calvin klein",
                    Description = "",
                    Price = Convert.ToDecimal(150.00),
                    UnitOfSize = ClothesUnitOfMeasure.Letter,
                    Variants = new List<Variant>()
                    {
                        new() { Size = ClothesSizes.LetterSizes["S"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["S"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["M"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["M"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["M"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["L"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["L"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["XL"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["XL"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["XL"], Status = VariantStatus.Warehouse },
                        new() { Size = ClothesSizes.LetterSizes["XXL"], Status = VariantStatus.Warehouse },
                    }
                },
                new()
                {
                    Name = "Koszula vistula",
                    Description = "Zaloz koszule vistule i badz w remizach krolem",
                    Price = Convert.ToDecimal(420.00),
                    UnitOfSize = ClothesUnitOfMeasure.Number,
                    Variants = new List<Variant>()
                    {
                        new() { Size = 40, Status = VariantStatus.Warehouse },
                        new() { Size = 42, Status = VariantStatus.Warehouse },
                        new() { Size = 42, Status = VariantStatus.Warehouse },
                        new() { Size = 42, Status = VariantStatus.Warehouse },
                        new() { Size = 44, Status = VariantStatus.Warehouse },
                        new() { Size = 44, Status = VariantStatus.Warehouse },
                        new() { Size = 44, Status = VariantStatus.Warehouse },
                        new() { Size = 46, Status = VariantStatus.Warehouse },
                        new() { Size = 46, Status = VariantStatus.Warehouse },
                        new() { Size = 46, Status = VariantStatus.Warehouse },
                        new() { Size = 48, Status = VariantStatus.Warehouse },
                        new() { Size = 48, Status = VariantStatus.Warehouse },
                    }
                }
            };

            await _dbContext.Products.AddRangeAsync(products);
            await _dbContext.SaveChangesAsync();

            var now = _appDateTime.Now;


            var drops = new List<Drop>()
            {
                new()
                {
                    Name = "Active 1 $$$",
                    Description = "Description of the drop",
                    StartDateTime = now.AddDays(-1),
                    EndDateTime = now.AddDays(1),
                },
                new()
                {
                    Name = "Active 2 $$$",
                    Description = "Description of the drop",
                    StartDateTime = now.AddDays(-2),
                    EndDateTime = now.AddMinutes(5)
                },
                new()
                {
                    Name = "Incoming 1 $$$",
                    Description = "Description of the drop",
                    StartDateTime = now.AddDays(1),
                    EndDateTime = now.AddMinutes(5)
                },
                new()
                {
                    Name = "Incoming empty",
                    Description = "Description of the drop",
                    StartDateTime = now.AddDays(8),
                    EndDateTime = now.AddMinutes(10)
                },
                new()
                {
                    Name = "Incoming empty 2",
                    Description = "Description of the drop",
                    StartDateTime = now.AddDays(9),
                    EndDateTime = now.AddMinutes(11)
                },
                new()
                {
                    Name = "Incoming empty 3",
                    Description = "Description of the drop",
                    StartDateTime = now.AddDays(10),
                    EndDateTime = now.AddMinutes(12)
                }
            };

            var random = new Random();

            foreach (var drop in drops)
            {
                if (drop.Name.Contains("$$$"))
                {
                    var availableVariants = await _dbContext.Variants
                        .Where(v => v.Status == VariantStatus.Warehouse)
                        .ToListAsync();
                    var randomIndex1 = random.Next(availableVariants.Count);
                    var randomVariant1 = availableVariants[randomIndex1];
                    randomVariant1.Status = VariantStatus.Drop;

                    var randomIndex2 = 0;
                    while (randomIndex2 == 0)
                    {
                        var rnd = random.Next(availableVariants.Count);
                        if (rnd == randomIndex1)
                        {
                            continue;
                        }

                        randomIndex2 = rnd;
                    }

                    var randomVariant2 = availableVariants[randomIndex2];
                    randomVariant2.Status = VariantStatus.Drop;

                    var randomIndex3 = 0;
                    while (randomIndex3 == 0)
                    {
                        var rnd = random.Next(availableVariants.Count);
                        if (rnd == randomIndex1 || rnd == randomIndex2)
                        {
                            continue;
                        }

                        randomIndex3 = rnd;
                    }

                    var randomVariant3 = availableVariants[randomIndex3];
                    randomVariant3.Status = VariantStatus.Drop;

                    drop.DropItems = new[] { randomVariant1, randomVariant2, randomVariant3 }
                        .Select(x => new DropItem() { Status = DropItemStatus.Available, Variant = x })
                        .ToList();
                }


                await _dbContext.Drops.AddAsync(drop);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}