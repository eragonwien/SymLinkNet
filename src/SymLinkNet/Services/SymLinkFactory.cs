using System;
using System.Runtime.InteropServices;

namespace SymLinkNet.Services
{
    public interface ISymLinkFactory
    {
        ISymLink GetInstance();
    }

    public class SymLinkFactory : ISymLinkFactory
    {
        public ISymLink GetInstance()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsSymLink();
            }

            throw new NotSupportedException(RuntimeInformation.OSDescription);
        }
    }
}
