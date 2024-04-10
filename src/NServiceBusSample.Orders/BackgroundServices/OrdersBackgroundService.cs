using NServiceBus;
using NServiceBusSample.Contracts.Commands;

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
                ProductId = Guid.NewGuid(),
                Version = DateTime.Now
            };
            
            logger.LogInformation("Sending a new order with id {OrderId}", placerOrderCommand.OrderId);

            await messageSession.Send(placerOrderCommand, cancellationToken);
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            
        }
    }
}