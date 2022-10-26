
namespace CG.Cryptography;

/// <summary>
/// This class contains extension methods related to the <see cref="ICryptographer"/>
/// type.
/// </summary>
public static partial class CryptographerExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    #region Encryption

    /// <summary>
    /// This method encrypts the given string using AES.
    /// </summary>
    /// <param name="cryptography">The cryptography instance to use for 
    /// the operation.</param>
    /// <param name="key">The key to use for the operation.</param>
    /// <param name="iv">The IV to use for the operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns an encrypted string.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <exception cref="CryptographicException">This exception is thrown 
    /// whenever the operation fails to complete properly.</exception>
    public static async ValueTask<string> AesEncryptAsync(
        this ICryptographer cryptography,
        byte[] key,
        byte[] iv,
        string value,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameter(s) before attempting to use them.
        Guard.Instance().ThrowIfNull(cryptography, nameof(cryptography))
            .ThrowIfNull(key, nameof(key))
            .ThrowIfNull(iv, nameof(iv))
            .ThrowIfFalse(key.LongLength == 32, nameof(key))
            .ThrowIfFalse(iv.LongLength == 16, nameof(iv))
            .ThrowIfNullOrEmpty(value, nameof(value));

        try
        {
            // Log what we are about to do.
            cryptography.Logger.LogDebug(
                "Generating an AES algorithm instance"
                );

            // Create the algorithm
            using (var alg = Aes.Create())
            {

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Setting the key and block sizes"
                    );

                // Set the block and key sizes.
                alg.KeySize = 256;
                alg.BlockSize = 128;

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Setting the key and IV values"
                    );

                // Copy the Key and IV.
                alg.Key = key;
                alg.IV = iv;

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Creating an AES encryptor"
                    );

                // Create the encryptor.
                using (var enc = alg.CreateEncryptor())
                {
                    // Log what we are about to do.
                    cryptography.Logger.LogDebug(
                        "Creating a memory stream"
                        );

                    // Create a temporary stream.
                    using (var stream = new MemoryStream())
                    {
                        // Log what we are about to do.
                        cryptography.Logger.LogDebug(
                            "Creating a cryptography stream"
                            );

                        // Create a cryptographic stream.
                        using (var cryptoStream = new CryptoStream(
                            stream,
                            enc,
                            CryptoStreamMode.Write
                            ))
                        {
                            // Log what we are about to do.
                            cryptography.Logger.LogDebug(
                                "Creating a stream writer"
                                );

                            // Create a writer
                            using (var writer = new StreamWriter(
                                cryptoStream
                                ))
                            {
                                // Log what we are about to do.
                                cryptography.Logger.LogDebug(
                                    "Writing the plain bytes"
                                    );

                                // Write the text.
                                await writer.WriteAsync(
                                    value
                                    ).ConfigureAwait(false);
                            }

                            // Log what we are about to do.
                            cryptography.Logger.LogDebug(
                                "Reading the encrypted bytes"
                                );

                            // Get the bytes.
                            var encryptedBytes = stream.ToArray();

                            // Log what we are about to do.
                            cryptography.Logger.LogDebug(
                                "Converting the encrypted bytes to a base-64 string"
                                );

                            // Convert the bytes to base-64.
                            var encryptedValue = Convert.ToBase64String(
                                encryptedBytes
                                );

                            // Return the results.
                            return encryptedValue;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            cryptography.Logger.LogError(
                "Failed to encrypt a string with AES!"
                );

            // Provider better context.
            throw new CryptographyException(
                message: $"Failed to encrypt a string with AES!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method encrypts the given bytes using AES.
    /// </summary>
    /// <param name="cryptography">The cryptography instance to use for 
    /// the operation.</param>
    /// <param name="key">The key to use for the operation.</param>
    /// <param name="iv">The IV to use for the operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns the encrypted 
    /// bytes.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <exception cref="CryptographicException">This exception is thrown 
    /// whenever the operation fails to complete properly.</exception>
    public static async ValueTask<byte[]> AesEncryptAsync(
        this ICryptographer cryptography,
        byte[] key,
        byte[] iv,
        byte[] value,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameter(s) before attempting to use them.
        Guard.Instance().ThrowIfNull(cryptography, nameof(cryptography))
            .ThrowIfNull(key, nameof(key))
            .ThrowIfNull(iv, nameof(iv))
            .ThrowIfFalse(key.LongLength == 32, nameof(key))
            .ThrowIfFalse(iv.LongLength == 16, nameof(iv))
            .ThrowIfNull(value, nameof(value));

        try
        {
            // Log what we are about to do.
            cryptography.Logger.LogDebug(
                "Generating an AES algorithm instance"
                );

            // Create the algorithm
            using (var alg = Aes.Create())
            {
                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Setting the key and block sizes"
                    );

                // Set the block and key sizes.
                alg.KeySize = 256;
                alg.BlockSize = 128;

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Setting the key and IV values"
                    );

                // Copy the Key and IV.
                alg.Key = key;
                alg.IV = iv;

                // Create a place to hold the encrypted bytes.
                byte[] encrypted = Array.Empty<byte>();

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Creating an AES encryptor"
                    );

                // Create the encryptor.
                using (var enc = alg.CreateEncryptor())
                {
                    // Log what we are about to do.
                    cryptography.Logger.LogDebug(
                        "Creating a memory stream"
                        );

                    // Create a work buffer.
                    using (var mstream = new MemoryStream())
                    {
                        // Log what we are about to do.
                        cryptography.Logger.LogDebug(
                            "Creating a cryptography stream"
                            );

                        // Wrap the buffer in a cryptography stream.
                        using (var cryptoStream = new CryptoStream(
                            mstream,
                            enc,
                            CryptoStreamMode.Write
                            ))
                        {
                            // Log what we are about to do.
                            cryptography.Logger.LogDebug(
                                "Writing the plain bytes"
                                );

                            // Write to the stream.
                            await cryptoStream.WriteAsync(
                                value,
                                cancellationToken
                                ).ConfigureAwait(false);
                        }

                        // Log what we are about to do.
                        cryptography.Logger.LogDebug(
                            "Reading the encrypted bytes"
                            );

                        // Copy the encrypted bytes from the work buffer.
                        encrypted = mstream.ToArray();
                    }
                }

                // Return the results.
                return encrypted;
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            cryptography.Logger.LogError(
                "Failed to encrypt an array with AES!"
                );

            // Provider better context.
            throw new CryptographyException(
                message: $"Failed to encrypt an array with AES!",
                innerException: ex
                );
        }
    }

    #endregion

    #region Decryption

    /// <summary>
    /// This method decrypts the given string using AES.
    /// </summary>
    /// <param name="cryptography">The cryptography instance to use for the
    /// operation.</param>
    /// <param name="key">The key to use for the operation.</param>
    /// <param name="iv">The IV to use for the operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns a decrypted string.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <exception cref="CryptographicException">This exception is thrown 
    /// whenever the operation fails to complete properly.</exception>
    public static ValueTask<string> AesDecryptAsync(
        this ICryptographer cryptography,
        byte[] key,
        byte[] iv,
        string value,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameter(s) before attempting to use them.
        Guard.Instance().ThrowIfNull(cryptography, nameof(cryptography))
            .ThrowIfNull(key, nameof(key))
            .ThrowIfNull(iv, nameof(iv))
            .ThrowIfFalse(key.LongLength == 32, nameof(key))
            .ThrowIfFalse(iv.LongLength == 16, nameof(iv))
            .ThrowIfNullOrEmpty(value, nameof(value));

        try
        {
            // Log what we are about to do.
            cryptography.Logger.LogDebug(
                "Generating an AES algorithm instance"
                );

            // Create the algorithm
            using (var alg = Aes.Create())
            {

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Setting the key and block sizes"
                    );

                // Set the block and key sizes.
                alg.KeySize = 256;
                alg.BlockSize = 128;

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Setting the Key and IV values"
                    );

                // Copy the key and IV.
                alg.Key = key;
                alg.IV = iv;

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Creating an AES decryptor"
                    );

                // Create the decryptor.
                using (var dec = alg.CreateDecryptor())
                {
                    // Log what we are about to do.
                    cryptography.Logger.LogDebug(
                        "Converting the string to bytes"
                        );

                    // Convert the encrypted value to bytes.
                    var encryptedBytes = Convert.FromBase64String(
                        value
                        );

                    // Log what we are about to do.
                    cryptography.Logger.LogDebug(
                        "Creating a memory stream"
                        );

                    // Create a temporary stream.
                    using (var stream = new MemoryStream(
                        encryptedBytes
                        ))
                    {
                        // Log what we are about to do.
                        cryptography.Logger.LogDebug(
                            "Creating a cryptography stream"
                            );

                        // Create a cryptography stream.
                        using (var cryptoStream = new CryptoStream(
                            stream,
                            dec,
                            CryptoStreamMode.Read
                            ))
                        {
                            // Log what we are about to do.
                            cryptography.Logger.LogDebug(
                                "Creating a stream reader"
                                );

                            // Create a cryptography reader.
                            using (var reader = new StreamReader(
                                cryptoStream
                                ))
                            {
                                // Log what we are about to do.
                                cryptography.Logger.LogDebug(
                                    "Reading the plain text"
                                    );

                                // Get the decrypted text.
                                var plainText = reader.ReadToEnd();

                                // Return the results.
                                return ValueTask.FromResult(plainText);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            cryptography.Logger.LogError(
                "Failed to decrypt a string with AES!"
                );

            // Provider better context.
            throw new CryptographyException(
                message: $"Failed to decrypt a string with AES!",
                innerException: ex
                );
        }
    }

    // *******************************************************************

    /// <summary>
    /// This method decrypts the given bytes using AES.
    /// </summary>
    /// <param name="cryptography">The cryptography instance to use for the
    /// operation.</param>
    /// <param name="key">The key to use for the operation.</param>
    /// <param name="iv">The IV to use for the operation.</param>
    /// <param name="value">The value to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns the decrypted 
    /// bytes.</returns>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more arguments are missing, or invalid.</exception>
    /// <exception cref="CryptographicException">This exception is thrown 
    /// whenever the operation fails to complete properly.</exception>
    public static async ValueTask<byte[]> AesDecryptAsync(
        this ICryptographer cryptography,
        byte[] key,
        byte[] iv,
        byte[] value,
        CancellationToken cancellationToken = default
        )
    {
        // Validate the parameter(s) before attempting to use them.
        Guard.Instance().ThrowIfNull(cryptography, nameof(cryptography))
            .ThrowIfNull(key, nameof(key))
            .ThrowIfNull(iv, nameof(iv))
            .ThrowIfFalse(key.LongLength == 32, nameof(key))
            .ThrowIfFalse(iv.LongLength == 16, nameof(iv))
            .ThrowIfNull(value, nameof(value));

        try
        {
            // Log what we are about to do.
            cryptography.Logger.LogDebug(
                "Generating an AES algorithm instance"
                );

            // Create the algorithm
            using (var alg = Aes.Create())
            {

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Setting the key and block sizes"
                    );

                // Set the block and key sizes.
                alg.KeySize = 256;
                alg.BlockSize = 128;

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Setting the key and IV values"
                    );

                // Copy the Key and IV.
                alg.Key = key;
                alg.IV = iv;

                // Create a place to put the decrypted bytes.
                byte[] decrypted = Array.Empty<byte>();

                // Log what we are about to do.
                cryptography.Logger.LogDebug(
                    "Creating an AES decryptor"
                    );

                // Create the decryptor
                using (var dec = alg.CreateDecryptor())
                {
                    // Log what we are about to do.
                    cryptography.Logger.LogDebug(
                        "Creating a memory stream"
                        );

                    // Create a work buffer.
                    using (var mstream = new MemoryStream())
                    {
                        // Log what we are about to do.
                        cryptography.Logger.LogDebug(
                            "Creating a cryptography stream"
                            );

                        // Wrap the buffer in a cryptography stream. 
                        using (var cryptoStream = new CryptoStream(
                            mstream,
                            dec,
                            CryptoStreamMode.Write
                            ))
                        {
                            // Log what we are about to do.
                            cryptography.Logger.LogDebug(
                                "Writing the bytes"
                                );

                            // Write to the stream.
                            await cryptoStream.WriteAsync(
                                value,
                                cancellationToken
                                ).ConfigureAwait(false);
                        }

                        // Log what we are about to do.
                        cryptography.Logger.LogDebug(
                            "Reading the plain bytes"
                            );

                        // Copy the decrypted bytes from the work buffer.
                        decrypted = mstream.ToArray();
                    }
                }

                // Return the results.
                return decrypted;
            }
        }
        catch (Exception ex)
        {
            // Log what happened.
            cryptography.Logger.LogError(
                "Failed to decrypt an array with AES!"
                );

            // Provider better context.
            throw new CryptographyException(
                message: $"Failed to decrypt an array with AES!",
                innerException: ex
                );
        }
    }

    #endregion

    #endregion
}
