using DropShot.Application.Common;
using DropShot.Application.Drops;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Drops.Models;
using DropShot.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DropShot.Application.UnitTests.Drops;

public class DropsListConverterTests
{
    private readonly IDropsListConverter _dropsListConverter;

    public DropsListConverterTests()
    {
        var dateTimeServiceMock = new Mock<IAppDateTime>();
        dateTimeServiceMock.Setup(x => x.Now).Returns(DateTime.UtcNow);

        _dropsListConverter = new DropsListConverter(dateTimeServiceMock.Object);
    }

    [Fact]
    public void should_correct_return_active_drop()
    {
        var drop1 = new DropCardDto()
        {
            Id = 1,
            StartDateTime = DateTime.UtcNow.AddDays(-1),
            EndDateTime = DateTime.UtcNow.AddDays(1)
        };

        var vm = _dropsListConverter.ConvertDropsListToLandingPageVm(new List<DropCardDto>() { drop1 });

        vm.ActiveDrops.Should().Contain(drop1);
        vm.IncomingDrops.Should().BeEmpty();
    }

    [Fact]
    public void should_correct_return_incoming_drop()
    {
        var drop1 = new DropCardDto()
        {
            Id = 1,
            StartDateTime = DateTime.UtcNow.AddDays(1),
            EndDateTime = DateTime.UtcNow.AddDays(2)
        };

        var vm = _dropsListConverter.ConvertDropsListToLandingPageVm(new List<DropCardDto>() { drop1 });

        vm.ActiveDrops.Should().BeEmpty();
        vm.IncomingDrops.Should().Contain(drop1);
    }

    [Fact]
    public void should_not_return_finished_drop()
    {
        var drop1 = new DropCardDto()
        {
            Id = 1,
            StartDateTime = DateTime.UtcNow.AddDays(-3),
            EndDateTime = DateTime.UtcNow.AddDays(-2)
        };

        var vm = _dropsListConverter.ConvertDropsListToLandingPageVm(new List<DropCardDto>() { drop1 });

        vm.ActiveDrops.Should().BeEmpty();
        vm.IncomingDrops.Should().BeEmpty();
    }

    [Fact]
    public void should_return_separate_correctly_drops_and_discard_finished()
    {
        var drop1 = new DropCardDto()
        {
            Id = 1,
            StartDateTime = DateTime.UtcNow.AddDays(-1),
            EndDateTime = DateTime.UtcNow.AddDays(1)
        };
        var drop2 = new DropCardDto()
        {
            Id = 1,
            StartDateTime = DateTime.UtcNow.AddDays(1),
            EndDateTime = DateTime.UtcNow.AddDays(2)
        };
        var drop3 = new DropCardDto()
        {
            Id = 1,
            StartDateTime = DateTime.UtcNow.AddDays(-3),
            EndDateTime = DateTime.UtcNow.AddDays(-2)
        };
        
        var vm = _dropsListConverter.ConvertDropsListToLandingPageVm(new List<DropCardDto>() { drop1, drop2, drop3 });

        vm.ActiveDrops.Should().Contain(drop1);
        vm.IncomingDrops.Should().Contain(drop2);
    }

    [Fact]
    public void should_return_empty_vm()
    {
        var drops = new List<DropCardDto>();

        var vm = _dropsListConverter.ConvertDropsListToLandingPageVm(drops);

        vm.ActiveDrops.Should().BeEmpty();
        vm.IncomingDrops.Should().BeEmpty();
    }
}