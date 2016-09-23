using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcklenAvenue.Commands;
using BlingBag;
using Rewardle.Boilerplate.Domain.UserRoot.Commands;
using Rewardle.Boilerplate.Events;

namespace Rewardle.Boilerplate.Domain.UserRoot.DomainEventHandlers
{
    public class TestCreatedEventHandler : IBlingHandler<TestAggregateRootCreated>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public TestCreatedEventHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task Handle(TestAggregateRootCreated @event)
        {
            await _commandDispatcher.Dispatch(
                new EventHandlerSession(), new UpdateAnotherTestData(@event.Name));
        }
    }
}
