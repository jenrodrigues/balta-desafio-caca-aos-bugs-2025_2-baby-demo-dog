using BugStore.Models;
using MediatR;

public class GetCustomerQuery : IRequest<Customer>
{
    public Guid Id { get; set; }

    public GetCustomerQuery(Guid id)
    {
        Id = id;
    }
}