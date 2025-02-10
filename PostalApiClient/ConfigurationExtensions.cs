using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostalApiClient.Utilities;
using PostalApiClient.v1;

namespace PostalApiClient;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Add client for postal server
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddPostalApiClient(this IServiceCollection services, IConfiguration configuration)
        => services.AddPostalApiClient(configuration.GetSection("PostalClient"));

    /// <summary>
    /// Add client for postal server
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">Action for set options</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddPostalApiClient(this IServiceCollection services, Action<Options> configure)
        => services.AddPostalApiClient(null, configure);
    
    /// <summary>
    /// Add client for postal server
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection">Configuration section with options</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddPostalApiClient(this IServiceCollection services, IConfigurationSection configurationSection)
        => services.AddPostalApiClient(configurationSection, null);
    
    /// <summary>
    /// Add client for postal server
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationSection">Configuration section with options</param>
    /// <param name="configure">
    /// Action for set options.
    /// Override options from configuration section</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddPostalApiClient(this IServiceCollection services, IConfigurationSection? configurationSection, Action<Options>? configure)
    {
        if (configurationSection != null)
        {
            services.Configure<Options>(configurationSection);
        }
        
        if (configure != null)
        {
            services.PostConfigure<Options>(configure.Invoke);
        }

        services.AddSingleton<PostalWebhookVerifier>();
        
        return services
            .AddHttpClient<PostalClient>();
    }
}