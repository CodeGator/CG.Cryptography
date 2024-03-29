﻿
namespace CG.Cryptography;

/// <summary>
/// This class is a base implementation of the <see cref="ICryptographer"/>
/// interface.
/// </summary>
public abstract class CryptographerBase : ICryptographer
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the logger for the class.
    /// </summary>
    internal protected readonly ILogger<ICryptographer> _logger;
    
    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <inheritdoc/>
    public virtual ILogger<ICryptographer> Logger => _logger;

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="CryptographerBase"/>
    /// class.
    /// </summary>
    /// <param name="logger">The logger to use with this class.</param>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more parameters are missing, or invalid.</exception>
    protected CryptographerBase(
        ILogger<ICryptographer> logger
        )
    {
        // Validate the parameters before attempting to use them.
        Guard.Instance().ThrowIfNull(logger, nameof(logger));

        // Save the reference(s).
        _logger = logger;
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public virtual ValueTask<Tuple<byte[], byte[]>> GenerateKeyAndIVAsync(
        string password,
        string salt,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameter(s) before attempting to use them.
        Guard.Instance().ThrowIfNullOrEmpty(password, nameof(password))
            .ThrowIfNullOrEmpty(salt, nameof(salt));

        try
        {
            // Log what we are about to do.
            _logger.LogDebug(
                "Generating an AES algorithm instance"
                );

            // Create the AES algorithm.
            using var alg = Aes.Create();

            // Log what we are about to do.
            _logger.LogDebug(
                "Setting the key and block sizes"
                );

            // Set the block and key sizes.
            alg.KeySize = 256;
            alg.BlockSize = 128;

            // Log what we are about to do.
            _logger.LogDebug(
                "Converting the password to bytes"
                );

            // Convert the password to bytes.
            var passwordBytes = Encoding.UTF8.GetBytes(
                password
                );

            // Log what we are about to do.
            _logger.LogDebug(
                "Converting the salt to bytes"
                );

            // Convert the salt to bytes.
            var saltBytes = Encoding.UTF8.GetBytes(
                salt
                );

            // Log what we are about to do.
            _logger.LogDebug(
                "Deriving the RFC2898 based cryptographic key"
                );

            // Derive the Key and IV.
            var derivedkey = new Rfc2898DeriveBytes(
                passwordBytes,
                saltBytes,
                10000
                );

            // Log what we are about to do.
            _logger.LogDebug(
                "Packaging the tuple with Key and IV"
                );

            // Return the results.
            var tuple = new Tuple<byte[], byte[]>(
                derivedkey.GetBytes(alg.KeySize / 8),
                derivedkey.GetBytes(alg.BlockSize / 8)
                );

            // Return the results.
            return ValueTask.FromResult(tuple);
        }
        catch (Exception ex)
        {
            // Log what happened.
            _logger.LogError(
                ex,
                "Failed to generate a cryptographic Key and IV!"
                );

            // Provider better context.
            throw new CryptographyException(
                message: $"Failed to generate a cryptographic Key and IV!",
                innerException: ex
                );
        }
    }

    #endregion
}
