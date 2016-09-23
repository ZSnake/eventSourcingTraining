using System;

namespace Rewardle.Boilerplate.Domain.Services
{
    public static class SystemDateTime
    {
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}