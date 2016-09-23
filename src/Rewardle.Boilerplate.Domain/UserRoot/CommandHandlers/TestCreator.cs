using System;
using System.Threading.Tasks;
using AcklenAvenue.Commands;
using Rewardle.Boilerplate.Domain.Services.Interfaces;
using Rewardle.Boilerplate.Domain.UserRoot.Commands;

namespace Rewardle.Boilerplate.Domain.UserRoot.CommandHandlers
{
    public class TestCreator : ICommandHandler<IUserSession, CreateTest>
    {

        readonly IAggregateRepository<TestAggregateRoot> _repository;

        public TestCreator(
            IAggregateRepository<TestAggregateRoot> repository)
        {
            _repository = repository;
        }

        public async Task Handle(IUserSession userIssuingCommand, CreateTest command)
        {
            var test = new TestAggregateRoot(
                Guid.NewGuid(),
                command.Name);
            await _repository.SaveChanges(test);
        }
    }
}