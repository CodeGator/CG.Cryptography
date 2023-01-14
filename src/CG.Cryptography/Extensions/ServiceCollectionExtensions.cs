
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This class contains extension methods related to the <see cref="ServiceCollection"/>
/// type.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds the types required to support the <see cref="ICryptographer"/>
    /// type and any extensions to that type.
    /// </summary>
    /// <param name="serviceCollection">The web application builder to
    /// use for the operation.</param>
    /// <param name="serviceLifetime">The service lifetime to use for the
    /// operation.</param>
    /// <returns>The value of the <paramref name="serviceCollection"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static IServiceCollection AddCryptography(
        this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime = ServiceLifetime.Singleton
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

        // Register the object.
        serviceCollection.Add<ICryptographer, Cryptographer>(serviceLifetime);

        // Return the builder.
        return serviceCollection;
    }

    #endregion
}
