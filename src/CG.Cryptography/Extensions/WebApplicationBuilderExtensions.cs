
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// This class contains extension methods related to the <see cref="WebApplicationBuilder"/>
/// type.
/// </summary>
public static partial class WebApplicationBuilderExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method adds the types required to support the <see cref="ICryptography"/>
    /// type and any extensions to that type.
    /// </summary>
    /// <param name="webApplicationBuilder">The web application builder to
    /// use for the operation.</param>
    /// <param name="serviceLifetime">The service lifetime to use for the
    /// operation.</param>
    /// <returns>The value of the <paramref name="webApplicationBuilder"/>
    /// parameter, for chaining calls together, Fluent style.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    public static WebApplicationBuilder AddCryptography(
        this WebApplicationBuilder webApplicationBuilder,
        ServiceLifetime serviceLifetime = ServiceLifetime.Singleton
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(webApplicationBuilder, nameof(webApplicationBuilder));

        // Register the manager.
        webApplicationBuilder.Services.Add<ICryptography, Cryptography>(serviceLifetime);

        // Return the builder.
        return webApplicationBuilder;
    }

    #endregion
}
