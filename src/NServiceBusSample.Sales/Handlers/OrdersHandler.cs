using NServiceBus;
using NServiceBusSample.Contracts.Commands;

namespace NServiceBusSample.Sales.Handlers;

public class OrdersHandler(ILogger<OrdersHandler> logger) 
    : IHandleMessages<PlacerOrderCommand>
{

    public Task Handle(PlacerOrderCommand message, IMessageHandlerContext context)
    {
        
        logger.LogInformation("The order with id {OrderId} has been processed", message.OrderId);
        return Task.CompletedTask;

    }
    
}