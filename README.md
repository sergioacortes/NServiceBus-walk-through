# NServiceBus-walk-through

This solution contains the projects used by the NServiceBus walk through sample.

- NServiceBusSample.Orders, this project is the responsible of post new order on the system.
- NServiceBusSample.Sales, this project is responsible of create the order and dispatch the order completed message.
- NServiceBusSample.Billing, this project is responsible of react to the order completed message to process the billing.

# Change project type to ASP.NET Core web application

# Configure NServiceBus

# Configure RabbitMq for NServiceBus