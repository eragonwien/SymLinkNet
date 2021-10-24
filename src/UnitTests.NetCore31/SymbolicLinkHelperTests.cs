using NUnit.Framework;
using SymLinkNet.Extensions;
using System.IO;
using System.Threading.Tasks;

namespace UnitTests.NetCore31
{
    [TestFixture]
    public class SymbolicLinkHelperTests
    {
        private readonly DirectoryInfo targetDirectoryInfo = new DirectoryInfo(@"C:\temp\symlinkNet\target");
        private readonly DirectoryInfo symLinkDirectoryInfo = new DirectoryInfo(@"C:\temp\symlinkNet\symlink");
        private readonly FileInfo targetFileInfo = new FileInfo(@"C:\temp\symlinkNet\target\test.txt");
        private readonly FileInfo symLinkFileInfo = new FileInfo(@"C:\temp\symlinkNet\symlink.txt");

        [SetUp]
        [TearDown]
        public void SetUp()
        {
            targetDirectoryInfo.Refresh();

            if (targetDirectoryInfo.Exists)
                targetDirectoryInfo.Delete(true);

            symLinkDirectoryInfo.Refresh();

            if (symLinkDirectoryInfo.IsSymbolicLink())
                symLinkDirectoryInfo.Delete(true);

            targetFileInfo.Refresh();

            if (targetFileInfo.Exists)
                targetFileInfo.Delete();

            symLinkFileInfo.Refresh();

            if (symLinkFileInfo.IsSymbolicLink())
                symLinkFileInfo.Delete();
        }

        [Test]
        public void DirectoryCreateSymbolicLinkTest()
        {
            // Arrange
            targetDirectoryInfo.Create();

            // Act
            targetDirectoryInfo.CreateSymbolicLink(symLinkDirectoryInfo.FullName);

            // Assert
            symLinkDirectoryInfo.Refresh();
            Assert.IsTrue(symLinkDirectoryInfo.IsSymbolicLink());
        }

        [Test]
        public void DirectoryGetSymbolicLinkRealPathTest()
        {
            // Arrange
            targetDirectoryInfo.Create();

            // Act
            targetDirectoryInfo.CreateSymbolicLink(symLinkDirectoryInfo.FullName);

            // Assert
            symLinkDirectoryInfo.Refresh();
            Assert.AreEqual(targetDirectoryInfo.FullName, symLinkDirectoryInfo.GetRealPath());
        }

        [Test]
        public async Task FileCreateSymbolicLinkTest()
        {
            // Arrange
            await this.CreateTargetFileInfoAsync();

            // Act
            targetFileInfo.CreateSymbolicLink(symLinkFileInfo.FullName);

            // Assert
            symLinkFileInfo.Refresh();
            Assert.IsTrue(symLinkFileInfo.IsSymbolicLink());
        }

        [Test]
        public async Task FileGetSymbolicLinkRealPathTest()
        {
            // Arrange
            await this.CreateTargetFileInfoAsync();

            // Act
            targetFileInfo.CreateSymbolicLink(symLinkFileInfo.FullName);

            // Assert
            symLinkFileInfo.Refresh();
            Assert.AreEqual(targetFileInfo.FullName, symLinkFileInfo.GetRealPath());
        }

        private async Task CreateTargetFileInfoAsync()
        {
            targetFileInfo.Directory.Create();

            using var fs = targetFileInfo.Create();
            using var sw = new StreamWriter(fs);
            sw.Write("this is a test");
            sw.Close();
            fs.Close();
        }
    }
}