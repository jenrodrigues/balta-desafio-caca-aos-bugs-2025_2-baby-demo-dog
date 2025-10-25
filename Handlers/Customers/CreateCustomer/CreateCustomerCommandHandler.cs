using BugStore.Data;
using BugStore.Models;
using MediatR;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
{
    private readonly AppDbContext _context;

    public CreateCustomerCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer { Id = new Guid(), Name = request.Name, Email = request.Email, Phone = request.Phone, BirthDate = request.BirthDate };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return customer;
    }
}