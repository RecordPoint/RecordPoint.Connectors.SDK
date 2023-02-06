using Microsoft.AspNetCore.DataProtection;
using System;
using System.Security;
using System.Text;

namespace RecordPoint.Connectors.SDK.Secrets
{
    /// <summary>
    /// Secret extension methods used to encrypt and decrypt secrets
    /// </summary>
    public static class EncryptionExtensions
    {
        private const string AppName = "CrossProtected";
        private const string BaseName = "CrossProtected_";

        private static readonly byte[] _SecondaryEntropy = { 6, 4, 3, 9, 55 };

        /// <summary>
        /// Encrypt a secret
        /// </summary>
        /// <param name="plainText">Secret in plain text</param>
        /// <returns>Encrypted secrety</returns>
        public static byte[] EncryptSecret(string plainText)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));

            var data = Encoding.ASCII.GetBytes(plainText);
            var protector = GetProtector(_SecondaryEntropy);

            return protector.Protect(data);
        }

        /// <summary>
        /// Decrypt a secret
        /// </summary>
        /// <param name="encryptedSecret">Previously encrypted secret</param>
        /// <returns>Decryptes plain text</returns>
        public static string DecryptSecret(byte[] encryptedSecret)
        {
            if (encryptedSecret == null || encryptedSecret.Length <= 0) throw new ArgumentNullException(nameof(encryptedSecret));
            var protector = GetProtector(_SecondaryEntropy);

            var data = protector.Unprotect(encryptedSecret);
            return Encoding.ASCII.GetString(data);
        }

        /// <summary>
        /// Convert plainText into a secure string
        /// </summary>
        /// <param name="plainText">Secret in plain text</param>
        /// <returns>Secure text</returns>
        public static SecureString GetSecureSecret(string plainText)
        {
            if (plainText == null) throw new ArgumentNullException(nameof(plainText));

            var secureStr = new SecureString();

            foreach (var c in plainText)
                secureStr.AppendChar(c);

            secureStr.MakeReadOnly();
            return secureStr;
        }

        private static IDataProtector GetProtector(byte[] optionalEntropy)
        {
            var provider = DataProtectionProvider.Create(AppName);
            var purpose = CreatePurpose(optionalEntropy);
            return provider.CreateProtector(purpose);
        }

        private static string CreatePurpose(byte[] optionalEntropy)
        {
            var result = BaseName + Convert.ToBase64String(optionalEntropy);
            return Uri.EscapeDataString(result);
        }
    }
}