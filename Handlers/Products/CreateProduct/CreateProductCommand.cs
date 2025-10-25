using BugStore.Models;
using MediatR;

public class CreateProductCommand : IRequest<Product>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public decimal Price { get; set; }
}