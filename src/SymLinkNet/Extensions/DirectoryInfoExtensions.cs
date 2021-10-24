using System.IO;

namespace SymLinkNet.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static void CreateSymbolicLink(this DirectoryInfo directoryInfo, string linkPath, bool overwrite = false)
            => SymLink.CreateSymbolicLink(linkPath, directoryInfo.FullName, overwrite);

        public static bool TryCreateSymbolicLink(this DirectoryInfo directoryInfo, string linkPath, bool overwrite = false)
            => SymLink.TryCreateSymbolicLink(linkPath, directoryInfo.FullName, overwrite);

        public static string GetRealPath(this DirectoryInfo directoryInfo)
            => SymLink.GetRealPath(directoryInfo.FullName);

        public static bool IsSymbolicLink(this DirectoryInfo directoryInfo)
            => SymLink.IsSymbolicLink(directoryInfo.FullName);
    }
}
