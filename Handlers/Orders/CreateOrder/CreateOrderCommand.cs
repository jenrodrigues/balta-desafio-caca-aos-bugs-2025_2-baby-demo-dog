using BugStore.Models;
using MediatR;

public class CreateOrderCommand : IRequest<Order>
{
    public Guid CustomerId { get; set; }
    public List<OrderLineDTO> Lines { get; set; } = null;
}

public class OrderLineDTO
{
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
}