namespace ECommerce.Api.Controllers;

using ECommerce.Application.Features.Orders.Commands.CreateOrder;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Application.Features.Orders.Queries.GetAllOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Protegido: Solo usuarios logueados pueden interactuar con órdenes
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand command)
    {
        // REGLA DE SEGURIDAD: Evitamos que un usuario cree una orden a nombre de otro
        // Obtenemos el ID del usuario directamente del Token JWT
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (command.UserId.ToString() != userIdFromToken)
        {
            return Forbid("No puedes crear una orden para otro usuario.");
        }

        var orderId = await _mediator.Send(command);
        
        // Retornamos 201 Created con el ID generado
        return Created($"/api/orders/{orderId}", new { OrderId = orderId });
    }
    // GET: api/orders/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetOrderByIdQuery(id));
        
        if (response is null)
            return NotFound();

        // VALIDACIÓN DE SEGURIDAD: Un usuario normal solo puede ver SUS PROPIAS órdenes.
        // Si es Admin, puede ver la de cualquiera.
        var userIdFromToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        if (userRole != "Admin" && response.UserId.ToString() != userIdFromToken)
        {
            return Forbid("No tienes permiso para ver esta orden.");
        }

        return Ok(response);
    }
    // GET: api/orders
    [HttpGet]
    [Authorize(Roles = "Admin")] // REGLA CRÍTICA: Solo el administrador puede ver las ventas globales
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(response);
    }
}