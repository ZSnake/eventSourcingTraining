using System.Threading.Tasks;
using Rewardle.Boilerplate.Domain;
using Rewardle.Boilerplate.Domain.UserRoot;
using Rewardle.Boilerplate.Events;

namespace Rewardle.Boilerplate.Projections
{
    public class TestProjection : IProjection<TestAggregateRootCreated>
    {
        readonly IViewModelWriter _viewStore;

        public TestProjection(IViewModelWriter viewStore)
        {
            _viewStore = viewStore;
        }

        public async Task Handle(TestAggregateRootCreated @event)
        {
            var testViewModel = new TestViewModel(@event.Name);
            await _viewStore.UpdateModel();
        }
    }
}