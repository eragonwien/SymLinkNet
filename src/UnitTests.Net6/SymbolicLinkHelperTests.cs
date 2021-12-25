using NUnit.Framework;
using SymLinkNet.Extensions;
using System.IO;
using System.Threading.Tasks;

namespace UnitTests.Net5
{
    [TestFixture]
    public class SymbolicLinkHelperTests
    {
        private readonly DirectoryInfo targetDirectoryInfo = new("tmp\\target");
        private readonly DirectoryInfo symLinkDirectoryInfo = new("tmp\\symlink");
        private readonly FileInfo targetFileInfo = new("tmp\\target.txt");
        private readonly FileInfo symLinkFileInfo = new("tmp\\symlink.txt");

        [SetUp]
        [TearDown]
        public void SetUp()
        {
            if (Directory.Exists("tmp"))
                Directory.Delete("tmp", true);
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
            await sw.WriteAsync("this is a test");
            sw.Close();
            fs.Close();
        }
    }
}