using BugStore.Data;
using BugStore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order?>
{
    private readonly AppDbContext _context;

    public GetOrderQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
                            .Include(o => o.Customer)
                            .Include(o => o.Lines).ThenInclude(line => line.Product)
                            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        return order;
    }
}