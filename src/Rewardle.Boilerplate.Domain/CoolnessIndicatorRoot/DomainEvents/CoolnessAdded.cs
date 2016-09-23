using System;
using Rewardle.Boilerplate.Events;

namespace Rewardle.Boilerplate.Domain.UserRoot.DomainEvents
{
    public class CoolnessAdded : IDomainEvent
    {
        public CoolnessAdded(Guid id, int coolness)
        {
            AggregateRootId = id;
            Coolness = coolness;
        }

        public int Coolness { get; set; }

        public Guid AggregateRootId { get; private set; }
    }
}