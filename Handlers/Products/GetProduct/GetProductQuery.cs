using BugStore.Models;
using MediatR;

public class GetProductQuery : IRequest<Product>
{
    public Guid Id { get; set; }

    public GetProductQuery(Guid id)
    {
        Id = id;
    }
}