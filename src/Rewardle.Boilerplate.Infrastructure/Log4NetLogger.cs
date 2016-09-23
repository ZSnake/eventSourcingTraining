using System;
using log4net;
using Rewardle.Boilerplate.Domain.Services;

namespace Rewardle.Boilerplate.Infrastructure
{
    public class Log4NetLogger : ILogger
    {
        public void Warning(string message, Exception exception, object sender)
        {
            ILog log = LogManager.GetLogger(sender.GetType());
            log.Warn(message, exception);
        }

        public void Error(string message, Exception exception, object sender)
        {
            ILog log = LogManager.GetLogger(sender.GetType());
            log.Error(message, exception);
        }
    }
}