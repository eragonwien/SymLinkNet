using Eragonwien.SymLinkNet.Eragonwien.SymLinkNet;
using NUnit.Framework;

namespace UnitTests.Net461
{
    [TestFixture]
    public class SymbolicLinkHelperTests
    {
        [Test]
        public void DirectoryCreateSymbolicLinkTest()
        {
            SymLink.CreateSymbolicLink("", "");
        }

        [Test]
        public void DirectoryGetSymbolicLinkTargetPathTest()
        {
            SymLink.GetRealPath("");
        }

        [Test]
        public void DirectoryIsSymbolicLinkTest()
        {
            SymLink.IsSymbolicLink("");
        }
    }
}
