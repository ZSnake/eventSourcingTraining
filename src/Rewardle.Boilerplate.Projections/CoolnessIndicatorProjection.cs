using System.Threading.Tasks;
using Rewardle.Boilerplate.Domain;
using Rewardle.Boilerplate.Domain.UserRoot;
using Rewardle.Boilerplate.Domain.UserRoot.DomainEvents;

namespace Rewardle.Boilerplate.Projections
{
    public class CoolnessIndicatorProjection : IProjection<CoolnessAdded>
    {
        readonly IViewModelWriter _viewStore;

        public CoolnessIndicatorProjection(IViewModelWriter viewStore)
        {
            _viewStore = viewStore;
        }

        public async Task Handle(CoolnessAdded @event)
        {
            var coolnessIndicatorViewModel = new CoolnessIndicatorViewModel(@event.Coolness);
            await _viewStore.UpdateModel();
        }
    }
}