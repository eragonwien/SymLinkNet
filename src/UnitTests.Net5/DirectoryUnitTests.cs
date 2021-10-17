using NUnit.Framework;

namespace UnitTests.Net5
{
    [TestFixture]
    public class DirectoryUnitTests
    {
        [Test]
        public void CreateSymbolicLinkTest()
        {
            HoangSonNguyen.SymbolicLinkHelper.Directory.CreateSymbolicLink("", "");
        }

        [Test]
        public void GetSymbolicLinkTargetPathTest()
        {
            HoangSonNguyen.SymbolicLinkHelper.Directory.GetSymbolicLinkTargetPath("");
        }

        [Test]
        public void IsSymbolicLinkTest()
        {
            HoangSonNguyen.SymbolicLinkHelper.Directory.IsSymbolicLink("");
        }
    }
}