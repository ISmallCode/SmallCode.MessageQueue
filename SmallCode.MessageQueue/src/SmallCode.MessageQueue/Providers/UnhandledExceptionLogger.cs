using Microsoft.Extensions.Logging;
using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Providers
{
    public class UnhandledExceptionLogger : ILogger
    {
        private readonly ILogService _service;

        public UnhandledExceptionLogger(ILogService service)
        {
            _service = service;
        }


        public IDisposable BeginScope<TState>(TState state)
        => new NoOpDisposable();

        public bool IsEnabled(LogLevel logLevel) =>
            logLevel == LogLevel.Critical || logLevel == LogLevel.Error;


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                Log log = new Log
                {
                    Exception = exception == null ? "" : exception.Message,
                    Level = Level.异常,
                };
                _service.Save(log);
            }
        }

        private sealed class NoOpDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
