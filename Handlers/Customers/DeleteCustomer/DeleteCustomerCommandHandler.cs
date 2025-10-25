using BugStore.Data;
using MediatR;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteCustomerCommandHandler(AppDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.Id);

        if (customer == null)
            return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}