using NServiceBus;
using NServiceBusSample.Contracts;

namespace NServiceBusSample.Orders.BackgroundServices;

public class OrdersBackgroundService(IMessageSession messageSession, ILogger<OrdersBackgroundService> logger) : BackgroundService
{
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {

            var placerOrderCommand = new PlacerOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                Description = $"New order",
                ProductId = Guid.NewGuid()
            };
            
            logger.LogInformation("Sending a new order with id {OrderOrderId}", placerOrderCommand.OrderId);

            await messageSession.Send(placerOrderCommand, cancellationToken);
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            
        }
    }
}