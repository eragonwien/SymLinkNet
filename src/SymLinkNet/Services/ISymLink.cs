namespace SymLinkNet.Services
{
    public interface ISymLink
    {
        void CreateSymbolicLink(string linkPath, string targetPath, bool overwrite = false);
        bool TryCreateSymbolicLink(string linkPath, string targetPath, bool overwrite = false);
        string GetRealPath(string linkPath);
        bool IsSymbolicLink(string linkPath);
    }
}
