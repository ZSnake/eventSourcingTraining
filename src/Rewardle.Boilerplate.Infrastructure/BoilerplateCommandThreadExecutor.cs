using AcklenAvenue.Commands;
using AcklenAvenue.Queueing;
using Newtonsoft.Json;
using Rewardle.Core.Infrastructure.CommandQueue;
using Rewardle.Core.Infrastructure.Worker;

namespace Rewardle.Boilerplate.Infrastructure
{
    public class BoilerplateCommandThreadExecutor : CommandThreadExecuter
    {
        public BoilerplateCommandThreadExecutor(IMessageDeleter<CommandQueueMessage> deleter, ICommandDispatcher commandDispatcher)
            : base(deleter, commandDispatcher)
        {
        }

        protected override JsonSerializer GetSerializerSettings()
        {
            var jsonSerializer = new JsonSerializer();
            return jsonSerializer;
        }
    }
}