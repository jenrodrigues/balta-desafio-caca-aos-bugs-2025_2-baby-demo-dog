using BugStore.Data;
using BugStore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<Customer>>
{
    private readonly AppDbContext _context;

    public GetCustomersQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers.ToListAsync(cancellationToken);
    }
}