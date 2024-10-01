using Microsoft.Extensions.Configuration;

namespace FinanceFlow.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsTestEnvoriment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
}
