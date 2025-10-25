using BugStore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(connectionString));
builder.Services.AddMediatR(typeof(Program));
var app = builder.Build();

app.MapGet("/", () => "Desafio Caça aos Bugs 2025");

app.MapGet("/v1/customers", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetCustomersQuery());
    return Results.Ok(result);
});

app.MapGet("/v1/customers/{id}", async (IMediator mediator, Guid id) =>
{
    var customer = await mediator.Send(new GetCustomerQuery(id));
    return customer is null ? Results.NotFound() : Results.Ok(customer);
});

app.MapPost("/v1/customers", async (IMediator mediator, CreateCustomerCommand command) =>
{
    var result = await mediator.Send(command);
    return Results.Created($"/v1/customers/{result.Id}", result);
});

app.MapPut("/v1/customers/{id}", async (IMediator mediator, Guid id, UpdateCustomerCommand command) =>
{
    command.Id = id;
    var result = await mediator.Send(command);

    return result is null ? Results.NotFound() : Results.Ok(result);
});

app.MapDelete("/v1/customers/{id}", async (IMediator mediator, Guid id) =>
{
    var result = await mediator.Send(new DeleteCustomerCommand { Id = id });

    return result ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/v1/products", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetProductsQuery());
    return Results.Ok(result);
});

app.MapGet("/v1/products/{id}", async (IMediator mediator, Guid id) =>
{
    var product = await mediator.Send(new GetProductQuery(id));
    return product is null ? Results.NotFound() : Results.Ok(product);
});

app.MapPost("/v1/products", async (IMediator mediator, CreateProductCommand command) =>
{
    var result = await mediator.Send(command);
    return Results.Created($"/v1/products/{result.Id}", result);
});

app.MapPut("/v1/products/{id}", async (IMediator mediator, Guid id, UpdateProductCommand command) =>
{
    command.Id = id;
    var result = await mediator.Send(command);

    return result is null ? Results.NotFound() : Results.Ok(result);
});


app.MapDelete("/v1/products/{id}", async (IMediator mediator, Guid id) =>
{
    var result = await mediator.Send(new DeleteProductCommand { Id = id });

    return result ? Results.NoContent() : Results.NotFound();
});

app.MapGet("/v1/orders/{id}", async (IMediator mediator, Guid id) =>
{
    var order = await mediator.Send(new GetOrderQuery(id));
    return order is null ? Results.NotFound() : Results.Ok(order);
});

app.MapPost("/v1/orders", async (IMediator mediator, CreateOrderCommand command) =>
{
    var result = await mediator.Send(command);

    if (result is null)
        return Results.BadRequest("Failed to create order.");

    return Results.Created($"/v1/orders/{result.Id}", result);
});

app.Run();
