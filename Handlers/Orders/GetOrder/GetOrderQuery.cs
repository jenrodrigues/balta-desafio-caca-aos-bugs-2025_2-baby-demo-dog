using BugStore.Models;
using MediatR;

public class GetOrderQuery : IRequest<Order>
{
    public Guid Id { get; set; }

    public GetOrderQuery(Guid id)
    {
        Id = id;
    }
}