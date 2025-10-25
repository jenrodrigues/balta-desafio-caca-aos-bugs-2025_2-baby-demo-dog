using BugStore.Data;
using BugStore.Models;
using MediatR;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer?>
{
    private readonly AppDbContext _context;

    public GetCustomerQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync([request.Id], cancellationToken);

        return customer;
    }
}