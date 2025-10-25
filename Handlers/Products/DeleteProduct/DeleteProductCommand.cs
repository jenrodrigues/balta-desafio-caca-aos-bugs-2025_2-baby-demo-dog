using MediatR;

public class DeleteProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}