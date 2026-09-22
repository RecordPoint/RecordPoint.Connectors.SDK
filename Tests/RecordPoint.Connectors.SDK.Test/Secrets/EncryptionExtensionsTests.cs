#nullable enable
using System;
using System.Runtime.InteropServices;
using System.Security;
using RecordPoint.Connectors.SDK.Secrets;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Secrets
{
    public class EncryptionExtensionsTests
    {
        [Fact]
        public void EncryptSecret_ThrowsOnNullOrEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => EncryptionExtensions.EncryptSecret(null!));
            Assert.Throws<ArgumentNullException>(() => EncryptionExtensions.EncryptSecret(string.Empty));
        }

        [Fact]
        public void DecryptSecret_ThrowsOnNullOrEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => EncryptionExtensions.DecryptSecret(null!));
            Assert.Throws<ArgumentNullException>(() => EncryptionExtensions.DecryptSecret(Array.Empty<byte>()));
        }

        [Fact]
        public void EncryptDecrypt_RoundTrips()
        {
            const string plainText = "MySecretValue123";
            var encrypted = EncryptionExtensions.EncryptSecret(plainText);
            Assert.NotNull(encrypted);
            Assert.NotEmpty(encrypted);

            var decrypted = EncryptionExtensions.DecryptSecret(encrypted);
            Assert.Equal(plainText, decrypted);
        }

        [Fact]
        public void GetSecureSecret_ThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => EncryptionExtensions.GetSecureSecret(null!));
        }

        [Fact]
        public void GetSecureSecret_ReturnsReadOnlySecureString()
        {
            const string plainText = "abc";
            SecureString secure = EncryptionExtensions.GetSecureSecret(plainText);
            Assert.True(secure.IsReadOnly());
            Assert.Equal(plainText.Length, secure.Length);

            var ptr = Marshal.SecureStringToBSTR(secure);
            try
            {
                Assert.Equal(plainText, Marshal.PtrToStringBSTR(ptr));
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
