namespace NServiceBusSample.Configuration.OpenTelemetry;

public interface ICaptureBodySelector
{
    bool CaptureBodyEnable();
}