using System;

namespace Rewardle.Boilerplate.Events
{
    public interface IDomainEvent
    {
        Guid AggregateRootId { get; }
    }
}