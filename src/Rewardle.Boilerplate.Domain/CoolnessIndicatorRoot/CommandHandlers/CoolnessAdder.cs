using System;
using System.Threading.Tasks;
using AcklenAvenue.Commands;
using Rewardle.Boilerplate.Domain.Services.Interfaces;
using Rewardle.Boilerplate.Domain.UserRoot.Commands;

namespace Rewardle.Boilerplate.Domain.UserRoot.CommandHandlers
{
    public class CoolnessAdder : ICommandHandler<IUserSession, AddCoolness>
    {

        readonly IAggregateRepository<CoolnessIndicator> _repository;

        public CoolnessAdder(
            IAggregateRepository<CoolnessIndicator> repository)
        {
            _repository = repository;
        }

        public async Task Handle(IUserSession userIssuingCommand, AddCoolness command)
        {
            var coolness = new CoolnessIndicator(Guid.NewGuid(), command.Coolness);
            await _repository.SaveChanges(coolness);
        }
    }
}