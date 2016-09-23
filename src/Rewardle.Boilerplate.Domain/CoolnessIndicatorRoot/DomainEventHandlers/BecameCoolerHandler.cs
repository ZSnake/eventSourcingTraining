using System.Threading.Tasks;
using AcklenAvenue.Commands;
using BlingBag;
using Rewardle.Boilerplate.Domain.UserRoot.Commands;
using Rewardle.Boilerplate.Events;

namespace Rewardle.Boilerplate.Domain.UserRoot.DomainEventHandlers
{
    public class BecameCoolerHandler : IBlingHandler<BecameCooler>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public BecameCoolerHandler(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public async Task Handle(BecameCooler @event)
        {
            await _commandDispatcher.Dispatch(new EventHandlerSession(), new AddCoolness(@event.Coolness));
        }
    }
}