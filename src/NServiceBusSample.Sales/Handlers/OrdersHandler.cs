using NServiceBus;
using NServiceBusSample.Contracts.Commands;
using NServiceBusSample.Contracts.Events;

namespace NServiceBusSample.Sales.Handlers;

public class OrdersHandler(ILogger<OrdersHandler> logger) 
    : IHandleMessages<PlacerOrderCommand>
{

    public async Task Handle(PlacerOrderCommand message, IMessageHandlerContext context)
    {

        var orderPlacedEvent = new OrderPlacedEvent()
        {
            Id = Guid.NewGuid(),
            OrderId = message.OrderId,
            Description = message.Description,
            ProductId = message.ProductId,
            Version = DateTime.UtcNow
        };
        
        logger.LogInformation("The order with id {OrderId} has been processed", message.OrderId);
        
        await context.Publish(orderPlacedEvent);

    }
    
}