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

            throw new PlatformNotSupportedException(RuntimeInformation.OSDescription);
        }

        public static ISymLink GetInstance(IServiceProvider sp) => GetInstance();
    }
}
