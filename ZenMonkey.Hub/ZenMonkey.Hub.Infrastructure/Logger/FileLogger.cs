using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMonkey.Hub.Infrastructure.Logger
{
    public class FileLogger : ILogger<FileLogger>
    {
        protected readonly FileLoggerProvider _loggerProvider;

        public FileLogger([NotNull] FileLoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var fullFilePath = _loggerProvider.Options.FolderPath + $"\\log_{DateTime.Now.ToString("ddMMyyyy")}.log";
            var excep = exception != null ? exception.StackTrace : string.Empty;
            var logRecord = string.Format($"[{DateTime.Now.ToString()}] {logLevel.ToString()} {formatter(state, exception)} {excep}");

            using var streamWritter = new StreamWriter(fullFilePath, true);
            streamWritter.WriteLine(logRecord);
        }
    }
}
