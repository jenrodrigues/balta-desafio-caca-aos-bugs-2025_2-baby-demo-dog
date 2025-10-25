using BugStore.Data;
using BugStore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly AppDbContext _context;

    public CreateOrderCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = Guid.NewGuid();
        var productIds = request.Lines.Select(l => l.ProductId).ToList();
        var productPrices = await _context.Products.Where(p => productIds.Contains(p.Id)).ToDictionaryAsync(p => p.Id, p => p.Price);

        var orderLines = request.Lines.Select(orderLine => new OrderLine
        {
            OrderId = orderId,
            Quantity = orderLine.Quantity,
            ProductId = orderLine.ProductId,
            Total = productPrices.TryGetValue(orderLine.ProductId, out var price) ? price * orderLine.Quantity : 0m
        }).ToList();

        var order = new Order
        {
            Id = orderId,
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.Now,
            Lines = orderLines
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        var savedOrder = await _context.Orders
                            .Include(o => o.Customer)
                            .Include(o => o.Lines)
                            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        return savedOrder;
    }
}