using System;
using AcklenAvenue.Commands;

namespace Rewardle.Boilerplate.Domain
{
    public class EventHandlerSession : IUserSession
    {
        public Guid Id
        {
            get { return Guid.NewGuid(); }
        }
    }
}