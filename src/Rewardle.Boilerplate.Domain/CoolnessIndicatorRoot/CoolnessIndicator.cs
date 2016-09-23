using System;
using System.Collections.Generic;
using AcklenAvenue.EventSourcing;
using Rewardle.Boilerplate.Domain.UserRoot.DomainEvents;

namespace Rewardle.Boilerplate.Domain.UserRoot
{
    public class CoolnessIndicator : AggregateRoot
    {

        public CoolnessIndicator(Guid id, int coolness) : base(new List<object>())
        {
            When(NewEvent(new CoolnessAdded(id, coolness)));
        }
        public CoolnessIndicator(IEnumerable<object> events) : base(events)
        {

        }

        private void When(CoolnessAdded newEvent)
        {
            Id = newEvent.AggregateRootId;
            Coolness = newEvent.Coolness;
        }

        public int Coolness { get; protected set; }

        public Guid Id { get; protected set; }
    }
}