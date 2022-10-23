
namespace CG.Cryptography;

/// <summary>
/// This interface represents an object that performs cryptographic operations.
/// </summary>
public interface ICryptography
{
    /// <summary>
    /// This property contains a logger for cryptographic operations.
    /// </summary>
    ILogger<ICryptography> Logger { get; }

    /// <summary>
    /// This method generates a Key and IV from the given password and salt.
    /// </summary>
    /// <param name="password">The password to use for the operation.</param>
    /// <param name="salt">The salt to use for the operation.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A task to perform the operation that returns a cryptographic
    /// key and IV value, in a tuple, as: (key, IV).</returns>
    ValueTask<(byte[], byte[])> GenerateKeyAndIVAsync(
        string password,
        string salt,
        CancellationToken cancellationToken = default
        );
}
