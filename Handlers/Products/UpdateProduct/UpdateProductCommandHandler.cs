using BugStore.Data;
using BugStore.Models;
using MediatR;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly AppDbContext _context;

    public UpdateProductCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id);

        if (product == null)
            return null;

        product.Title = request.Title;
        product.Description = request.Description;
        product.Slug = request.Slug;
        product.Price = request.Price;
        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }
}