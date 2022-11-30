using System.Text;
using DropShot.Domain.Entities;
using DropShot.Infrastructure.DAL;
using DropShot.Infrastructure.Identity.Models;
using Newtonsoft.Json;

namespace DropShotApi.IntegrationTests.Common;

public static class Utilities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static DropShotDbContext Context { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public static void InitializeDbForTests(DropShotDbContext context)
    {
        Context = context;
        ResetDb();
        SeedForTests();
    }

    public static StringContent GetRequestContent(object obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }

    public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(stringResponse);

        return result;
    }

    private static void ResetDb()
    {
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }

    private static void SeedForTests()
    {
        SeedUsers();
    }

    private static void SeedUsers()
    {
    }

}