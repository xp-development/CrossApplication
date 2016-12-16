using CrossApplication.Core.Common.Security;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Security._StringEncryption
{
    public class Encrypt
    {
        [Theory]
        [InlineData("1es11", "、怅瀃、、")]
        [InlineData("䕸", "瑘")]
        public void Usage(string value, string result)
        {
            new StringEncryption().Encrypt(value).Should().Be(result);
        }
    }
}