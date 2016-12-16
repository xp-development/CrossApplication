using System;
using System.Text;
using CrossApplication.Core.Contracts.Security;

namespace CrossApplication.Core.Common.Security
{
    public class StringEncryption : IStringEncryption
    {
        public string Encrypt(string value)
        {
            var result = new StringBuilder();
            foreach (var t in value)
            {
                var first = (byte) ((t & 0xFF00) >> 8);
                var second = (byte) (t & 0xFF);
                result.Append(BitConverter.ToChar(new []{(byte) (((byte) (first & 0xF) << 4) | (byte)(second & 0xF)), (byte) (((byte)(second >> 4) << 4) | (byte)(first >> 4))}, 0));
            }

            return result.ToString();
        }

        public string Decrypt(string value)
        {
            var result = new StringBuilder();
            foreach (var t in value)
            {
                var first = (byte)((t & 0xFF00) >> 8);
                var second = (byte)(t & 0xFF);
                result.Append(BitConverter.ToChar(new[] { (byte)((first >> 4 << 4) | second & 0xF), (byte)(((byte)(first & 0xF) << 4) | second >> 4) }, 0));
            }

            return result.ToString();
        }
    }
}