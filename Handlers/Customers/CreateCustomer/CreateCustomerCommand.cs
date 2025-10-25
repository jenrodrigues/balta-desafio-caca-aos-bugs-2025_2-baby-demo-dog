using BugStore.Models;
using MediatR;

public class CreateCustomerCommand : IRequest<Customer>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
}