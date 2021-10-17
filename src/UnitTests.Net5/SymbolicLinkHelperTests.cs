using HoangSonNguyen.IO.Helpers;
using NUnit.Framework;

namespace UnitTests.Net5
{
    [TestFixture]
    public class SymbolicLinkHelperTests
    {
        [Test]
        public void CreateSymbolicLinkTest()
        {
            SymbolicLinkHelper.Directory.CreateSymbolicLink("", "");
        }

        [Test]
        public void GetSymbolicLinkTargetPathTest()
        {
            SymbolicLinkHelper.Directory.GetSymbolicLinkTargetPath("");
        }

        [Test]
        public void IsSymbolicLinkTest()
        {
            SymbolicLinkHelper.Directory.IsSymbolicLink("");
        }

        [Test]
        public void FileCreateSymbolicLinkTest()
        {
            SymbolicLinkHelper.File.CreateSymbolicLink("", "");
        }

        [Test]
        public void FileGetSymbolicLinkTargetPathTest()
        {
            SymbolicLinkHelper.File.GetSymbolicLinkTargetPath("");
        }

        [Test]
        public void FileIsSymbolicLinkTest()
        {
            SymbolicLinkHelper.File.IsSymbolicLink("");
        }
    }
}