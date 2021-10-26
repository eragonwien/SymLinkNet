using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SymLinkNet.Services
{
    public class BaseSymLink
    {
        protected bool FileOrDirectoryExists(string path)
            => Directory.Exists(path) || File.Exists(path);

        protected void EnsureOsIsSupported(OSPlatform osPlatform)
        {
            if (!RuntimeInformation.IsOSPlatform(osPlatform))
                throw new PlatformNotSupportedException($"This method supports {osPlatform} only");
        }

        protected void EnsureFileOrDirectoryExists(string path)
        {
            if (!FileOrDirectoryExists(path))
                throw new IOException($"File or directory at {path} not found");
        }

        protected void EnsureFileOrDirectoryNotExists(string path)
        {
            if (FileOrDirectoryExists(path))
                throw new IOException($"File or directory already exists at {path}");
        }

        protected void DeleteSymbolicLink(string linkPath)
        {
            if (Directory.Exists(linkPath))
                Directory.Delete(linkPath, true);

            if (File.Exists(linkPath))
                File.Delete(linkPath);
        }

        public virtual bool IsSymbolicLink(string path)
        {
            if (!FileOrDirectoryExists(path))
                return false;

            return File.GetAttributes(path).HasFlag(FileAttributes.ReparsePoint);
        }
    }
}
