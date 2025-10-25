using MediatR;

public class DeleteCustomerCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}