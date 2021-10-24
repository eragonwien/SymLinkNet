using SymLinkNet.Models;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SymLinkNet.Services
{
    public static class CommandLineUltility
    {
        public static CommandLineResult ExecuteCommand(string cmdText)
        {
            if (string.IsNullOrWhiteSpace(cmdText))
                throw new ArgumentException("Command cannot be null or whitespace");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return ExecuteWindowsCommand(cmdText);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return ExecuteLinuxCommand(cmdText);
            }

            throw new NotSupportedException($"{RuntimeInformation.OSDescription} is not supported");
        }

        private static CommandLineResult ExecuteLinuxCommand(string cmdText)
        {
            throw new NotImplementedException();
        }

        private static CommandLineResult ExecuteWindowsCommand(string cmdText)
        {
            if (cmdText is null) throw new ArgumentNullException(nameof(cmdText));

            try
            {
                using var process = new Process();

                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/C {cmdText}";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();
                process.WaitForExit();

                return CommandLineResult.Success(process);
            }
            catch (Exception ex)
            {
                return CommandLineResult.Error(ex);
            }
        }
    }
}
