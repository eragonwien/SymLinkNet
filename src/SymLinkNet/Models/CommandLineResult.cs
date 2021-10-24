using System;
using System.Diagnostics;

namespace SymLinkNet.Models
{
    public sealed class CommandLineResult
    {
        public bool Succeed { get; set; }
        public string Output { get; set; }
        public int ExitCode { get; set; }
        public Exception Exception { get; set; }

        public static CommandLineResult Success(Process process)
        {
            if (process is null) throw new ArgumentNullException(nameof(process));

            return new CommandLineResult
            {
                Succeed = true,
                Output = process.StandardOutput.ReadToEnd(),
                ExitCode = process.ExitCode,
            };
        }

        public static CommandLineResult Error(Exception exception)
        {
            return new CommandLineResult
            {
                Succeed = false,
                Output = exception.Message,
                Exception = exception
            };
        }
    }
}
