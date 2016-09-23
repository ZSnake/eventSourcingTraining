using System;
using System.Collections.Generic;
using AcklenAvenue.EventSourcing;
using Rewardle.Boilerplate.Domain.UserRoot.Commands;
using Rewardle.Boilerplate.Events;

namespace Rewardle.Boilerplate.Domain.UserRoot
{
    public class TestAggregateRoot : AggregateRoot
    {

        public TestAggregateRoot(Guid id, string name) : base(new List<object>())
        {
            When(
                NewEvent(new TestAggregateRootCreated(id, name)));
        }

        private void When(TestAggregateRootCreated newEvent)
        {
            Id = newEvent.AggregateRootId;
            Name = newEvent.Name;
        }

        protected TestAggregateRoot() : base(new List<object>())
        {
        }

        protected TestAggregateRoot(IEnumerable<object> events) : base(events)
        {
        }

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        
    }


}