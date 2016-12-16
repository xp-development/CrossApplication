using CrossApplication.Core.Common.Security;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Security._StringEncryption
{
    public class Decrypt
    {
        [Theory]
        [InlineData("、怅瀃、、", "1es11")]
        [InlineData("瑘", "䕸")]
        public void Usage(string value, string result)
        {
            new StringEncryption().Decrypt(value).Should().Be(result);
        }
    }
}