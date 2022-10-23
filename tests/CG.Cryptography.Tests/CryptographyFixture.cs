
namespace CG.Cryptography
{
    /// <summary>
    /// This class is a test fixture for the <see cref="Cryptography"/> class.
    /// </summary>
    [TestClass]
    [TestCategory("Unit")]
    public class CryptographyFixture
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is a unit test that verifies the <see cref="Cryptography.GenerateKeyAndIVAsync(string, System.Threading.CancellationToken)"/> 
        /// method. Here we verify that the method generates a valid Key and IV
        /// using the given test password.
        /// </summary>
        [TestMethod]
        public async Task Cryptography_GenerateKeyAndIV()
        {
            // Arrange ...
            var logger = new Mock<ILogger<ICryptography>>();
            var crypto = new Cryptography(logger.Object);

            // Act ...
            var result1 = await crypto.GenerateKeyAndIVAsync(
                "password",
                "salt"
                ).ConfigureAwait(false);

            // Assert ...
            Assert.IsTrue(
                result1.Item1.Length == 32,
                "The Key is the wrong length!"
                );
            Assert.IsTrue(
                result1.Item2.Length == 16,
                "The IV is the wrong length!"
                );
        }

        #endregion
    }
}
