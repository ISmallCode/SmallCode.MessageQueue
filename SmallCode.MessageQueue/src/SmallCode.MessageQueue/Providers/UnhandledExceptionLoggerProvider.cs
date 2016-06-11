using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmallCode.MessageQueue.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Providers
{
    public class UnhandledExceptionLoggerProvider : ILoggerProvider
    {
        private readonly ILogService _repo;

        public UnhandledExceptionLoggerProvider(ILogService repo)
        {
            _repo = repo;
        }

        public ILogger CreateLogger(string categoryName) =>
            new UnhandledExceptionLogger(_repo);

        public void Dispose()
        {
        }
    }
}
