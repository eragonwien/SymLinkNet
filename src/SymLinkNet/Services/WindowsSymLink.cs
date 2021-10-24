using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Principal;

namespace SymLinkNet.Services
{
    public class WindowsSymLink : ISymLink
    {
        #region WinAPI

        [DllImport("kernel32.dll", EntryPoint = "CreateSymbolicLinkW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool CreateSymbolicLink([In] string lpSymlinkFileName, [In] string lpTargetFileName, [In] SymbolicLinkType dwFlags);

        [DllImport("kernel32.dll", EntryPoint = "GetFinalPathNameByHandleW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetFinalPathNameByHandle([In] IntPtr hFile, [Out] StringBuilder lpszFilePath, [In] int cchFilePath, [In] int dwFlags);

        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr SecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        private const int CREATION_DISPOSITION_OPEN_EXISTING = 3;
        private const int FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;

        enum SymbolicLinkType
        {
            File = 0,
            Directory = 1
        }

        #endregion

        public void CreateSymbolicLink(string linkPath, string targetPath, bool overwrite = false)
        {
            EnsureOsIsWindows();
            EnsureFileOrDirectoryExists(targetPath);

            if (overwrite)
                DeleteSymbolicLink(linkPath);

            EnsureFileOrDirectoryNotExists(linkPath);

            if (!HasAdminRights())
            {
                throw new UnauthorizedAccessException("The program requires administration permissions");
            }

            if (Directory.Exists(targetPath) &&
                CreateSymbolicLink(linkPath, targetPath, SymbolicLinkType.Directory))
                return;

            if (File.Exists(targetPath) &&
                CreateSymbolicLink(linkPath, targetPath, SymbolicLinkType.File))
                return;

            throw new Win32Exception(Marshal.GetLastWin32Error());
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

        public string GetRealPath(string linkPath)
        {
            DirectoryInfo symlink = new DirectoryInfo(linkPath);
            SafeFileHandle directoryHandle = CreateFile(
                symlink.FullName,
                0,
                2,
                IntPtr.Zero,
                CREATION_DISPOSITION_OPEN_EXISTING,
                FILE_FLAG_BACKUP_SEMANTICS,
                IntPtr.Zero);

            if (directoryHandle.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            StringBuilder result = new StringBuilder(512);
            int mResult = GetFinalPathNameByHandle(directoryHandle.DangerousGetHandle(), result, result.Capacity, 0);

            if (mResult < 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            if (result.Length >= 4 && result[0] == '\\' && result[1] == '\\' && result[2] == '?' && result[3] == '\\')
                return result.ToString().Substring(4);// "\\?\" entfernen

            return result.ToString();
        }

        public bool IsSymbolicLink(string path)
        {
            if (!FileOrDirectoryExists(path))
                return false;

            return File.GetAttributes(path).HasFlag(FileAttributes.ReparsePoint);
        }

        private void DeleteSymbolicLink(string linkPath)
        {
            if (Directory.Exists(linkPath))
                Directory.Delete(linkPath, true);

            if (File.Exists(linkPath))
                File.Delete(linkPath);
        }

        private bool FileOrDirectoryExists(string path)
            => Directory.Exists(path) || File.Exists(path);

        private void EnsureOsIsWindows()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                throw new PlatformNotSupportedException($"This method supports Windows only");
        }

        private void EnsureFileOrDirectoryExists(string path)
        {
            if (!FileOrDirectoryExists(path))
                throw new IOException($"File or directory at {path} not found");
        }

        private void EnsureFileOrDirectoryNotExists(string path)
        {
            if (FileOrDirectoryExists(path))
                throw new IOException($"File or directory already exists at {path}");
        }

        private bool HasAdminRights()
        {
            using var winIdentity = WindowsIdentity.GetCurrent();
            var windowPrincipal = new WindowsPrincipal(winIdentity);

            return windowPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
