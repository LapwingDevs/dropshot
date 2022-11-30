using System.Net;
using DropShot.Application.Auth.Models;
using DropShot.Application.Products.Models;
using DropShot.Application.Users.Models;
using DropShot.Domain.Enums;
using DropShot.Infrastructure.DAL;
using DropShotApi.IntegrationTests.Common;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace DropShotApi.IntegrationTests.Controllers.Auth;

[Collection("Sequential")]
public class RegisterUser : IntegrationTest
{
    public RegisterUser(IntegrationTestFactory<Program, DropShotDbContext> factory, ITestOutputHelper output) : base(factory, output)
    {
    }
    
    [Fact]
    public async Task AddUserSuccessful()
    {
        string firstName = "John";
        string lastName = "Doe";
        string email = "test@test.com";

        var product = new RegisterUserDto()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = "#Zaq12wsx",
            Address = new AddressDto()
            {
                Line1 = "Test",
                Line2 = "Test",
                City = "Test",
                PostalCode = "Test",
            }
        };

        var response = await Client.PostAsync("/Auth/register", Utilities.GetRequestContent(product));
        response.EnsureSuccessStatusCode();
        var responseContent = await Utilities.GetResponseContent<RegisterResponseDto>(response);
        responseContent.User.FirstName.Should().Be(firstName);
        responseContent.User.LastName.Should().Be(lastName);
        responseContent.User.Email.Should().Be(email);
        responseContent.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task AddUserWithoutAddressBadRequest()
    {
        string firstName = "John";
        string lastName = "Doe";
        string email = "test@test.com";

        var product = new RegisterUserDto()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = "#Zaq12wsx",
        };

        var response = await Client.PostAsync("/Auth/register", Utilities.GetRequestContent(product));
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}