using Eragonwien.SymLinkNet;
using NUnit.Framework;

namespace UnitTests.NetCore21
{
    [TestFixture]
    public class SymbolicLinkHelperTests
    {
        [Test]
        public void CreateSymbolicLinkTest()
        {
            SymLink.CreateSymbolicLink("", "");
        }

        [Test]
        public void GetSymbolicLinkTargetPathTest()
        {
            SymLink.GetRealPath("");
        }

        [Test]
        public void IsSymbolicLinkTest()
        {
            SymLink.IsSymbolicLink("");
        }
    }
}
