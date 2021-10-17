using HoangSonNguyen.IO.Helpers;
using NUnit.Framework;

namespace UnitTests.Net461
{
    [TestFixture]
    public class SymbolicLinkHelperTests
    {
        [Test]
        public void DirectoryCreateSymbolicLinkTest()
        {
            SymbolicLinkHelper.Directory.CreateSymbolicLink("", "");
        }

        [Test]
        public void DirectoryGetSymbolicLinkTargetPathTest()
        {
            SymbolicLinkHelper.Directory.GetSymbolicLinkTargetPath("");
        }

        [Test]
        public void DirectoryIsSymbolicLinkTest()
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
