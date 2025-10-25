using BugStore.Data;
using BugStore.Models;
using MediatR;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
{
    private readonly AppDbContext _context;

    public UpdateCustomerCommandHandler(AppDbContext context) => _context = context;

    public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.Id);

        if (customer == null)
            return null;

        customer.Name = request.Name;
        customer.Email = request.Email;
        customer.Phone = request.Phone;
        customer.BirthDate = request.BirthDate;
        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }
}