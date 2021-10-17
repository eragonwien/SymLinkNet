using System;

namespace HoangSonNguyen.SymbolicLinkHelper
{
    public class FileSystemInfo
    {
        public bool IsSymbolicLink { get; }
        public string SymbolicLinkTargetPath { get; }

        public void CreateSymbolicLink(string linkPath)
        {
            throw new NotImplementedException();
        }
    }
}
