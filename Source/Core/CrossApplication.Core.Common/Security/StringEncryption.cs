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
            return Encoding.Unicode.GetString(ProtectedData.Protect(Encoding.Unicode.GetBytes(value), null, DataProtectionScope.CurrentUser));
        }

        public string Decrypt(string value)
        {
            return Encoding.Unicode.GetString(ProtectedData.Unprotect(Encoding.Unicode.GetBytes(value), null, DataProtectionScope.CurrentUser));
        }
    }
}