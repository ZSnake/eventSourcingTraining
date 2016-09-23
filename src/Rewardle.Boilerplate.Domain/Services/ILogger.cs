using System;

namespace Rewardle.Boilerplate.Domain.Services
{
    public interface ILogger
    {
        void Warning(string message, Exception exception, object sender);
        void Error(string message, Exception exception, object sender);
    }
}