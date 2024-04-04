namespace NServiceBusSample.Configuration.Configurations;

public class CaptureBodyConfig
{
    public CaptureBodyConfig() => this.Tenants = new List<string>();

    public bool EnableForAll { get; set; }

    public List<string> Tenants { get; set; }
}