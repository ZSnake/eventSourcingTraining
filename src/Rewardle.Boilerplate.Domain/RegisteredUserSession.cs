using System;
using AcklenAvenue.Commands;

namespace Rewardle.Boilerplate.Domain
{
    public class RegisteredUserSession : IUserSession
    {
        public RegisteredUserSession(Guid aggregateId)
        {
            Id = Guid.NewGuid();
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; private set; }
        public Guid Id { get; private set; }
    }
}