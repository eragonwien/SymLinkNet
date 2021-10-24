using System;
using System.Diagnostics;

namespace SymLinkNet.Models
{
    public sealed class CommandLineResult
    {
        public bool Succeed => Exception != null;
        public string Output { get; set; }
        public int ExitCode { get; set; }
        public Exception Exception { get; set; }

        public static CommandLineResult Success(Process process)
        {
            if (process is null) throw new ArgumentNullException(nameof(process));

            return new CommandLineResult
            {
                Output = process.StandardOutput.ReadToEnd(),
                ExitCode = process.ExitCode,
            };
        }

        public void EnsureSuccess()
        {
            if (!Succeed)
                throw Exception;
        }

        public static CommandLineResult Error(Exception exception)
        {
            return new CommandLineResult
            {
                Output = exception.Message,
                Exception = exception
            };
        }
    }
}
