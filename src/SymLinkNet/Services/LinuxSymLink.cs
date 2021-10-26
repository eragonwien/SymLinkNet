using SymLinkNet.Ultility;
using System;
using System.Runtime.InteropServices;

namespace SymLinkNet.Services
{
    public sealed class LinuxSymLink : BaseSymLink, ISymLink
    {
        public void CreateSymbolicLink(string linkPath, string targetPath, bool overwrite = false)
        {
            EnsureOsIsSupported(OSPlatform.Linux);
            EnsureFileOrDirectoryExists(targetPath);

            if (overwrite)
                DeleteSymbolicLink(linkPath);

            EnsureFileOrDirectoryNotExists(linkPath);

            EnsureFileOrDirectoryNotExists(linkPath);

            CommandLineUltility
                .ExecuteCommand($"ln -s '{targetPath}' '{linkPath}'")
                .EnsureSuccess();
        }

        public string GetRealPath(string linkPath)
        {
            var cmd = CommandLineUltility.ExecuteCommand($"readlink -f '{linkPath}'");

            cmd.EnsureSuccess();

            return cmd.Output;
        }

        public bool TryCreateSymbolicLink(string linkPath, string targetPath, bool overwrite = false)
        {
            try
            {
                CreateSymbolicLink(linkPath, targetPath, overwrite);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
