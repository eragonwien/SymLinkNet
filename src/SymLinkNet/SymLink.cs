using SymLinkNet.Services;

namespace SymLinkNet
{
    public static class SymLink
    {
        public static void CreateSymbolicLink(string linkPath, string targetPath, bool overwrite = false)
        {
            var symLink = GetInstance();

            symLink.CreateSymbolicLink(linkPath, targetPath, overwrite);
        }

        public static bool TryCreateSymbolicLink(string linkPath, string targetPath, bool overwrite = false)
        {
            var symLink = GetInstance();

            return symLink.TryCreateSymbolicLink(linkPath, targetPath);
        }

        public static string GetRealPath(string linkPath)
        {
            var symLink = GetInstance();

            return symLink.GetRealPath(linkPath);
        }

        public static bool IsSymbolicLink(string linkPath)
        {
            var symLink = GetInstance();

            return symLink.IsSymbolicLink(linkPath);
        }

        private static ISymLink GetInstance() => new SymLinkFactory().GetInstance();
    }
}
