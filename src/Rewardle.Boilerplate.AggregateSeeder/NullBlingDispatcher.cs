using System.Threading.Tasks;
using Rewardle.Core.Domain;

namespace Rewardle.Boilerplate.AggregateSeeder
{
    public class NullBlingDispatcher : IAsyncEventDispatcher
    {
        public async Task Dispatch(object @event)
        {
            
        }
    }
}