using BugStore.Data;
using BugStore.Models;
using MediatR;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly AppDbContext _context;

    public CreateProductCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product { Id = new Guid(), Title = request.Title, Description = request.Description, Slug = request.Slug, Price = request.Price };
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }
}