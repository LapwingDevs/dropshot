using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using DropShot.Application.Drops;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Drops.Models;


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
    public void should_sort_active_drops_correctly()
    {
        var drop1 = new DropCardDto()
        {
            Id = 1,
            StartDateTime = DateTime.UtcNow.AddDays(-1),
            EndDateTime = DateTime.UtcNow.AddDays(1)
        };
        var drop2 = new DropCardDto()
        {
            Id = 2,
            StartDateTime = DateTime.UtcNow.AddDays(-7),
            EndDateTime = DateTime.UtcNow.AddDays(2)
        };
        var drop3 = new DropCardDto()
        {
            Id = 3,
            StartDateTime = DateTime.UtcNow.AddDays(-3),
            EndDateTime = DateTime.UtcNow.AddHours(2)
        };
        
        var vm = _dropsListConverter.ConvertDropsListToLandingPageVm(new List<DropCardDto>() { drop1, drop2, drop3 });

        vm.ActiveDrops.ElementAt(0).Should().Be(drop3);
        vm.ActiveDrops.ElementAt(1).Should().Be(drop1);
        vm.ActiveDrops.ElementAt(2).Should().Be(drop2);
    }

    [Fact]
    public void should_sort_incoming_drops_correctly()
    {
        var drop1 = new DropCardDto()
        {
            Id = 1,
            StartDateTime = DateTime.UtcNow.AddDays(5),
            EndDateTime = DateTime.UtcNow.AddDays(7)
        };
        var drop2 = new DropCardDto()
        {
            Id = 2,
            StartDateTime = DateTime.UtcNow.AddDays(3),
            EndDateTime = DateTime.UtcNow.AddDays(15)
        };
        var drop3 = new DropCardDto()
        {
            Id = 3,
            StartDateTime = DateTime.UtcNow.AddHours(3),
            EndDateTime = DateTime.UtcNow.AddHours(2)
        };
        
        var vm = _dropsListConverter.ConvertDropsListToLandingPageVm(new List<DropCardDto>() { drop1, drop2, drop3 });

        vm.IncomingDrops.ElementAt(0).Should().Be(drop3);
        vm.IncomingDrops.ElementAt(1).Should().Be(drop2);
        vm.IncomingDrops.ElementAt(2).Should().Be(drop1);
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