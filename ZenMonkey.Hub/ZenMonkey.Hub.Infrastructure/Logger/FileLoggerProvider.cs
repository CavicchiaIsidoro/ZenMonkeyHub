using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenMonkey.Hub.Infrastructure.Logger
{
    [ProviderAlias("FileLogger")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public readonly FileLoggerOptions Options;
        private IConfiguration _configuration;

        public FileLoggerProvider(IOptions<FileLoggerOptions> options,
            IConfiguration configuration)
        {
            _configuration = configuration;
            Options = options.Value;
            Options.FolderPath = _configuration["FileLogger:Options:FolderPath"];
            Options.FilePath = _configuration["FileLogger:Options:FilePath"];

            if (!Directory.Exists($@"{Options.FolderPath}"))
            {
                Directory.CreateDirectory($@"{Options.FolderPath!}");
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }

        public void Dispose()
        {
        }
    }
}
