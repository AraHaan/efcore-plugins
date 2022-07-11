namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Relational Database plugin <see cref="IServiceCollection" /> extensions.
/// </summary>
public static class RelationalServiceCollectionExtensions
{
    /// <summary>
    /// Adds Relational Database Plugin Convensions to the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <returns>The same service collection to use for chaining.</returns>
    public static IServiceCollection AddRelationalExtensionsPlugin(
        this IServiceCollection serviceCollection)
    {
        ArgumentNullException.ThrowIfNull(serviceCollection);
        _ = new EntityFrameworkRelationalServicesBuilder(serviceCollection)
            .TryAdd<IConventionSetPlugin, RelationalExtensionsConvensionSetPlugin>();
        return serviceCollection;
    }
}
