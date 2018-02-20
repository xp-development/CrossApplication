using System;
using System.Security.Cryptography;
using System.Text;
using CrossApplication.Core.Contracts.Security;

namespace CrossApplication.Core.Common.Security
{
    public class StringEncryption : IStringEncryption
    {
        public string Encrypt(string value)
        {
            return BitConverter.ToString(ProtectedData.Protect(Encoding.Unicode.GetBytes(value), null, DataProtectionScope.CurrentUser)).Replace("-", "");
        }

        public string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Encoding.Unicode.GetString(ProtectedData.Unprotect(StringToByteArray(value), null, DataProtectionScope.CurrentUser));
        }

        private static byte[] StringToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }
    }
}