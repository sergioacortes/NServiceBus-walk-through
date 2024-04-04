using System.Diagnostics;
using NServiceBusSample.Configuration.Configurations;

namespace NServiceBusSample.Configuration.OpenTelemetry;

public class CaptureBodySelector : ICaptureBodySelector
{
    
    private readonly CaptureBodyConfig config;
    private const string tenantTag = "TenantId";

    public CaptureBodySelector(CaptureBodyConfig config)
    {
        this.config = config ?? throw new ArgumentNullException(nameof (config));
    }

    public bool CaptureBodyEnable()
    {
        
        bool flag = false;
        
        if (this.config.EnableForAll)
            return true;
        
        if (!this.config.Tenants.Any<string>())
            return false;
        
        if (Activity.Current.Baggage.Any<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => x.Key == "TenantId")))
            flag = this.config.Tenants.Contains(Activity.Current.Baggage.First<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (x => x.Key == "TenantId")).Value);
        
        return flag;
        
    }
}