using System;
using System.Runtime.InteropServices;

namespace SymLinkNet.Services
{
    public class SymLinkFactory
    {
        public static ISymLink GetInstance()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsSymLink();
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new LinuxSymLink();
            }

            throw new PlatformNotSupportedException(RuntimeInformation.OSDescription);
        }

        public static ISymLink GetInstance(IServiceProvider sp) => GetInstance();
    }
}
