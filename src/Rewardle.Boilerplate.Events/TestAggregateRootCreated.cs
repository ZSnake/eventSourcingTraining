using System;

namespace Rewardle.Boilerplate.Events
{
    public class TestAggregateRootCreated : IDomainEvent
    {
        public TestAggregateRootCreated(Guid aggregateRootId, string name)
        {
            AggregateRootId = aggregateRootId;
            Name = name;
        }

        public string Name { get; private set; }
        public Guid AggregateRootId { get; private set; }
    }
}