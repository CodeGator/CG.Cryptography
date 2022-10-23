
using System.IO;
using System.Text;

namespace CG.Cryptography.Extensions
{
    /// <summary>
    /// This class is a test fixture for the <see cref="CryptographyExtensions"/> class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class CryptographyExtensionsFixture
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains the test Key for the fixture.
        /// </summary>
        private byte[] _key;

        /// <summary>
        /// This field contains the test IV for the fixture.
        /// </summary>
        private byte[] _iv;

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method initializes the test fixture before a test run.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            // Create a Key and IV.
            var logger = new Mock<ILogger<ICryptography>>();
            var crypto = new Cryptography(logger.Object);
            var tuple = crypto.GenerateKeyAndIVAsync(
                "password",
                "salt"
                ).Result;

            // Save the results.
            _key = tuple.Item1;
            _iv = tuple.Item2;
        }

        // *******************************************************************

        #region Encrypt

        /// <summary>
        /// This method is a unit test that verifies the <see cref="CryptographyExtensions.AesEncryptAsync(ICryptography, byte[], byte[], string, System.Threading.CancellationToken)"/> 
        /// method. Here we verify that the method encrypts a string using our
        /// test credentials.
        /// </summary>
        [TestMethod]
        public async Task CryptographyExtensions_AesEncryptString()
        {
            // Arrange ...
            var logger = new Mock<ILogger<ICryptography>>();

            var crypto = new Cryptography(
                logger.Object
                ) as ICryptography;

            var rawValue = "codegator";

            // Act ...
            var result = await crypto.AesEncryptAsync(
                _key,
                _iv,
                rawValue
                ).ConfigureAwait(false);

            // Assert ...
            Assert.IsTrue(
                result.Length != 0,
                "The result is empty!"
                );
            Assert.IsTrue(
                result != rawValue,
                "The result wasn't encrypted!"
                );
            Assert.IsTrue(
                Convert.FromBase64String(result).Length > 0,
                "The result wasn't base64 encoded!"
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="CryptographyExtensions.AesEncryptAsync(ICryptography, byte[], byte[], byte[], System.Threading.CancellationToken)"/> 
        /// method. Here we verify that the method encrypts bytes using our
        /// test credentials.
        /// </summary>
        [TestMethod]
        public async Task CryptographyExtensions_AesEncryptBytes()
        {
            // Arrange ...
            var logger = new Mock<ILogger<ICryptography>>();

            var crypto = new Cryptography(
                logger.Object
                ) as ICryptography;

            var rawValue = new byte[] { 0, 1, 2, 3 };

            // Act ...
            var result = await crypto.AesEncryptAsync(
                _key, 
                _iv, 
                rawValue
                ).ConfigureAwait(false);

            // Assert ...
            Assert.IsTrue(
                result.Length != 0,
                "The result is empty!"
                );
            Assert.IsTrue(
                result[0] != rawValue[0],
                "The result wasn't encrypted!"
                );
        }

        #endregion

        // *******************************************************************

        #region Decrypt

        /// <summary>
        /// This method is a unit test that verifies the <see cref="CryptographyExtensions.AesDecryptAsync(ICryptography, byte[], byte[], byte[], System.Threading.CancellationToken)"/> 
        /// method. Here we verify that the method decrypts bytes that were 
        /// encrypted using our test credentials and the <see cref="CryptographyExtensions.AesEncryptAsync(ICryptography, byte[], byte[], byte[], System.Threading.CancellationToken)"/>
        /// method.
        /// </summary>
        [TestMethod]
        public async Task CryptographyExtensions_AesDecryptBytes()
        {
            // Arrange ...
            var logger = new Mock<ILogger<ICryptography>>();

            var crypto = new Cryptography(
                logger.Object
                ) as ICryptography;

            var rawValue = new byte[] { 0, 1, 2, 3 };

            var encryptedValue = await crypto.AesEncryptAsync(
                _key,
                _iv,
                rawValue
                ).ConfigureAwait(false);

            // Act ...
            var result = await crypto.AesDecryptAsync(
                _key,
                _iv,
                encryptedValue
                ).ConfigureAwait(false);

            // Assert ...
            Assert.IsTrue(
                result.Length != 0,
                "The result is empty!"
                );
            Assert.IsTrue(
                result.Length == rawValue.Length,
                "The result was invalid!"
                );
            Assert.IsTrue(
                result[0] == rawValue[0] &&
                result[1] == rawValue[1] &&
                result[2] == rawValue[2] &&
                result[3] == rawValue[3],
                "The result was invalid!"
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method is a unit test that verifies the <see cref="CryptographyExtensions.AesDecryptAsync(ICryptography, byte[], byte[], string, System.Threading.CancellationToken)"/> 
        /// method. Here we verify that the method decrypts a string that was 
        /// encrypted using our test credentials and the <see cref="CryptographyExtensions.AesEncryptAsync(ICryptography, byte[], byte[], string, System.Threading.CancellationToken)"/>
        /// method.
        /// </summary>
        [TestMethod]
        public async Task CryptographyExtensions_AesDecryptString()
        {
            // Arrange ...
            var logger = new Mock<ILogger<ICryptography>>();

            var crypto = new Cryptography(
                logger.Object
                ) as ICryptography;

            var rawValue = "codegator";

            var encryptedValue = await crypto.AesEncryptAsync(
                _key,
                _iv,
                rawValue
                ).ConfigureAwait(false);

            // Act ...
            var result = await crypto.AesDecryptAsync(
                _key,
                _iv,
                encryptedValue
                ).ConfigureAwait(false);

            // Assert ...
            Assert.IsTrue(
                result.Length != 0,
                "The result is empty!"
                );
            Assert.IsTrue(
                result == rawValue,
                "The result was invalid!"
                );
        }

        #endregion

        #endregion
    }
}
