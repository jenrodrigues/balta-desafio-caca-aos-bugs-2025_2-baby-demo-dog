using BugStore.Data;
using BugStore.Models;
using MediatR;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product?>
{
    private readonly AppDbContext _context;

    public GetProductQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync([request.Id], cancellationToken);

        return product;
    }
}