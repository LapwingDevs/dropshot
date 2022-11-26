using DropShot.Application.Common.Abstraction;
using DropShot.Application.Orders.Interfaces;
using DropShot.Application.Orders.Models;
using DropShot.Application.Users;
using DropShot.Application.Users.Interfaces;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Orders;

public class OrdersService : IOrdersService
{
    private readonly IUserService _userService;
    private readonly IDbContext _dbContext;

    public OrdersService(IDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    public async Task SubmitOrder(string applicationUserId, SubmitOrderRequest request)
    {
        var user = await _userService.GetUser(u => u.ApplicationUserId == applicationUserId);
        
        var orderItems = request.CartItems.Select(i => new OrderItem()
        {
            Price = i.ProductPrice,
            VariantId = i.VariantId
        }).ToList();

        var newOrder = new Order()
        {
            TotalPrice = Convert.ToDecimal(request.TotalPrice),
            ShippingCost = Convert.ToDecimal(request.ShippingCost),
            IsPaid = false,
            UserId = user.Id,
            OrderItems = orderItems,
        };

        await _dbContext.Orders.AddAsync(newOrder);

        await SetDropItemsStatusAsOrdered(orderItems.Select(oi => oi.VariantId));

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    public async Task SetOrderAsPaid(int orderId)
    {
        var order = await _dbContext.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
        if (order is null)
        {
            throw new Exception("");
        }

        order.IsPaid = true;

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    private async Task SetDropItemsStatusAsOrdered(IEnumerable<int> variantIds)
    {
        var dropItems = await _dbContext.DropItems
            .Where(i => variantIds.Contains(i.VariantId))
            .ToListAsync();

        // TODO: n+1?
        foreach (var dropItem in dropItems)
        {
            dropItem.Status = DropItemStatus.Ordered;
        }
    }
}