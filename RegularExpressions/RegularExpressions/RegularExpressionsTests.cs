using NUnit.Framework;
using System.Text.RegularExpressions;

namespace RegularExpressions
{
    public class RegularExpressionsTests
    {
        private readonly Regex ipRegex = new Regex(@"^(((0(3[0-7][0-7]|[0-2][0-7][0-7])|25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|\d|0x[0-9A-F]{2})\.){3}(25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|0x[0-9A-F]{2}|0(3[0-7][0-7]|[0-2][0-7][0-7])|\d)|0x[0-9A-F]{8}|429496729[0-5]|42949672[0-8]\d|4294967[0-1]\d{2}|429496[0-6]\d{3}|42949[0-5]\d{4}|4294[0-8]\d{5}|429[0-3]\d{6}|42[0-8]\d{7}|4[0-1]\d{8}|[1-3]\d{0,9}|0[1-3][0-7]{10})$");

        private readonly Regex domainNameRegex = new Regex(@"^https?://(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9-]{0,61}[a-zA-Z0-9])\.){1,120}([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9-]{0,61}[a-zA-Z0-9])/?$");

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("192.0.2.235")]
        [TestCase("99.198.122.146")]
        [TestCase("18.101.25.153")]
        [TestCase("23.71.254.72")]
        [TestCase("100.100.100.100")]
        [TestCase("173.194.34.134")]
        [TestCase("212.58.241.131")]
        [TestCase("46.51.197.88")]
        [TestCase("0xC0.0x00.0x02.0xEB")]
        [TestCase("0xFF.0x12.0xF1.0x1F")]
        [TestCase("0x11.0x22.0x33.0x44")]
        [TestCase("0300.0000.0002.0353")]
        [TestCase("0377.0377.0377.0377")]
        [TestCase("0100.0100.0100.0100")]
        [TestCase("0xC00002EC")]
        [TestCase("0xFF12F11F")]
        [TestCase("0x11223344")]
        [TestCase("3221226219")]
        [TestCase("2130706433")]
        [TestCase("287454020")]
        [TestCase("4279431455")]
        [TestCase("4294967295")]
        [TestCase("030000001353")]
        [TestCase("030000001354")]
        [TestCase("037704570437")]
        [TestCase("2110431504")]
        [TestCase("037777777777")]
        [TestCase("0300.19.0.2")]
        [TestCase("99.0377.4.0002")]
        [TestCase("0xFF.255.0377.0x12")]
        public void AssertIpRegexMatches(string ip)
        {
            Assert.That(ipRegex.Match(ip).Value, Is.EqualTo(ip), $"{ip} should match but wasn't");
        }

        [TestCase("256.256.256.256")]
        [TestCase("0x100.0x11.0x11.0x11")]
        [TestCase("0x11.0x100.0x11.0x11")]
        [TestCase("0xx20.0x20.0x20.0x20")]
        [TestCase("0180.0100.0100.0100")]
        [TestCase("0100.0100.0109.0100")]
        [TestCase("0x100111111")]
        [TestCase("0x111001111")]
        [TestCase("100.100.100")]
        [TestCase("0x20.0x50.0x2")]
        [TestCase(".100.100.100.100")]
        [TestCase("100..100.100.100.")]
        [TestCase("100.100.100.100.")]
        [TestCase("256.100.100.100.100")]
        [TestCase("100.100.100.100.0x40")]
        [TestCase("4294967296")]
        [TestCase("37777777778")]
        [TestCase("1,1377E+19")]
        public void AssertIpRegexDoesNotMatch(string ip)
        {
            Assert.That(ipRegex.Match(ip).Value, Is.EqualTo(string.Empty), $"{ip} should NOT match but was");
        }

        [TestCase("http://example.com/")]
        [TestCase("https://example.com/")]
        [TestCase("http://www.example.com/")]
        [TestCase("http://example.org/")]
        [TestCase("http://example.co.uk")]
        [TestCase("http://bla.bla.biz")]
        [TestCase("http://1.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.ip6.arpa")]
        [TestCase("https://boring.museum")]
        [TestCase("http://lynx.io/")]
        [TestCase("https://example.edu")]
        [TestCase("http://example.gov/")]
        [TestCase("http://sub.example.cd")]
        [TestCase("https://personal.test.me")]
        [TestCase("http://0test.com/")]
        [TestCase("http://a.b")]
        [TestCase("http://this-test.com")]
        [TestCase("http://test.this-test.com/")]
        [TestCase("http://this-test.test.com")]
        [TestCase("http://TESTdomain.com")]
        [TestCase("http://abcdefghijklmnopqrstuvwxyz.com")]
        public void AssertDomainNameRegexMatches(string testString)
        {
            Assert.That(domainNameRegex.Match(testString).Value, Is.EqualTo(testString), $"{testString} should NOT match but was");
        }

        [TestCase("example.com")]
        [TestCase("www.example.com")]
        [TestCase("invalid://example.com")]
        [TestCase("httpd://example.com/")]
        [TestCase("ahttp://example.com")]
        [TestCase("javascript:alert()")]
        [TestCase("mailto:nobody@example.com")]
        [TestCase("http://test ing.com")]
        [TestCase("http://test'ing.com")]
        [TestCase("http://test_ing.com")]
        [TestCase("http://inval.id,com")]
        [TestCase("http://")]
        [TestCase("http://.com")]
        [TestCase("http://.....com")]
        [TestCase("http://example..com")]
        [TestCase("http://example.com.")]
        [TestCase("http://-example.com")]
        [TestCase("http://example-.com")]
        [TestCase("http://example.com-")]
        [TestCase("http://1234567890123456789012345678901234567890123456789012345678901234.com")]
        [TestCase("http://0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.com")]
        public void AssertDomainNameDoesNotMatch(string testString)
        {
            Assert.That(domainNameRegex.Match(testString).Value, Is.EqualTo(string.Empty), $"{testString} should NOT match but was");
        }
    }
}