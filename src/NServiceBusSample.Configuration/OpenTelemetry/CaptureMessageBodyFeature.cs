using Microsoft.Extensions.DependencyInjection;
using NServiceBus.Features;

namespace NServiceBusSample.Configuration.OpenTelemetry;

public class CaptureMessageBodyFeature: Feature
{
    protected override void Setup(FeatureConfigurationContext context)
    {
        
        context.Services.AddSingleton<ICaptureBodySelector, CaptureBodySelector>();
        
        context
            .Pipeline
            .Register<IncomingMessagePayloadBehavior>(new IncomingMessagePayloadBehavior(), "OpenTelemetry Incoming Capture Body Behavior");
        
        context.Pipeline.Register<OutgoingMessagePayloadBehavior>(new OutgoingMessagePayloadBehavior(), "OpenTelemetry OutgoingCapture Body Behavior");
        
    }
}