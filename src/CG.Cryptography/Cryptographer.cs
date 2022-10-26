
namespace CG.Cryptography;

/// <summary>
/// This class is a default implementation of the <see cref="ICryptographer"/>
/// interface.
/// </summary>
internal class Cryptographer : CryptographerBase, ICryptographer
{
    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="Cryptographer"/>
    /// class.
    /// </summary>
    /// <param name="logger">The logger to use with this class.</param>
    /// <exception cref="ArgumentException">This exception is thrown whenever
    /// one or more parameters are missing, or invalid.</exception>
    public Cryptographer(
        ILogger<ICryptographer> logger
        ) : base(logger)
    {

    }

    #endregion
}
