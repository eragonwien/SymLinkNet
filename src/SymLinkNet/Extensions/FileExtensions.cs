using System;
using System.IO;

namespace SymLinkNet.Extensions
{
    public static class FileExtensions
    {
        public static void CreateSymbolicLink(this FileInfo fileInfo, string linkPath, bool overwrite = false)
            => SymLink.CreateSymbolicLink(linkPath, fileInfo.FullName, overwrite);

        public static bool TryCreateSymbolicLink(this FileInfo fileInfo, string linkPath, bool overwrite = false)
            => SymLink.TryCreateSymbolicLink(linkPath, fileInfo.FullName, overwrite);

        public static string GetRealPath(this FileInfo fileInfo)
            => SymLink.GetRealPath(fileInfo.FullName);

        public static bool IsSymbolicLink(this FileInfo fileInfo)
            => SymLink.IsSymbolicLink(fileInfo.FullName);
    }
}
