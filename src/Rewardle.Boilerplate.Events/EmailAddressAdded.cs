using System;

namespace Rewardle.Identity.Events
{
    public class EmailAddressAdded : IDomainEvent
    {
        public EmailAddressAdded(Guid aggregateRootId, string email, Guid emailValidationToken)
        {
            AggregateRootId = aggregateRootId;
            Email = email;
            EmailValidationToken = emailValidationToken;
        }

        public Guid AggregateRootId { get; private set; }
        public string Email { get; private set; }
        public Guid EmailValidationToken { get; private set; }
    }
}