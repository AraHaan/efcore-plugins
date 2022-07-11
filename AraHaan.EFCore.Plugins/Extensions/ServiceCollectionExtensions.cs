namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Non-Relational Database plugin <see cref="IServiceCollection" /> extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Non-Relational Database Plugin Convensions to the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <returns>The same service collection to use for chaining.</returns>
    public static IServiceCollection AddExtensionsPlugin(
        this IServiceCollection serviceCollection)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        _ = new EntityFrameworkServicesBuilder(serviceCollection)
            .TryAdd<IConventionSetPlugin, ExtensionsConvensionSetPlugin>();
        return serviceCollection;
    }
}
