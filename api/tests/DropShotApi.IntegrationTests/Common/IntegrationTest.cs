using System.Net.Http.Headers;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DropShot.API;
using DropShot.Infrastructure.DAL;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using Xunit.Abstractions;

namespace DropShotApi.IntegrationTests.Common;

public class IntegrationTest : IClassFixture<IntegrationTestFactory<Program, DropShotDbContext>>
{
    private readonly IntegrationTestFactory<Program, DropShotDbContext> Factory;
    
    protected readonly ITestOutputHelper _output;
    protected readonly HttpClient Client;

    public IntegrationTest(IntegrationTestFactory<Program, DropShotDbContext> factory, ITestOutputHelper output)
    {
        Factory = factory;
        _output = output;
        Client = Factory.CreateClient();
        
        //Client.DefaultRequestHeaders.Authorization =   new AuthenticationHeaderValue(scheme: "IntegrationTests");
    }
}