﻿using NServiceBus;
using NServiceBusSample.Contracts.Events;

namespace NServiceBusSample.Billing.Handlers;

public class OrdersHandler(ILogger<OrdersHandler> logger) : IHandleMessages<OrderPlacedEvent>
{

    public Task Handle(OrderPlacedEvent message, IMessageHandlerContext context)
    {
        
        logger.LogInformation("Processing the billing for the order {OrderId}", message.OrderId);

        return Task.CompletedTask;
        
    }
    
}