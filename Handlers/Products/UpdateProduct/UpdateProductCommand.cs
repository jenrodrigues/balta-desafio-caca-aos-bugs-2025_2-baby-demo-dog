using BugStore.Models;
using MediatR;

public class UpdateProductCommand : IRequest<Product>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public decimal Price { get; set; }
}