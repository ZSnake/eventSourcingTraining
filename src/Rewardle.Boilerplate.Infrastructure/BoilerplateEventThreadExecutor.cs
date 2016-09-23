using System;
using System.Collections.Generic;
using AcklenAvenue.Queueing;
using BlingBag;
using Rewardle.Core.Infrastructure.EventQueue;
using Rewardle.Core.Infrastructure.Worker;

namespace Rewardle.Boilerplate.Infrastructure
{
    public class BoilerplateEventThreadExecutor : EventThreadExecuter
    {
        readonly IBlingDispatcher _blingDispatcher;

        public BoilerplateEventThreadExecutor(IBlingDispatcher blingDispatcher, IMessageDeleter<EventQueueMessage> messageDeleter) : base(blingDispatcher, messageDeleter)
        {
            _blingDispatcher = blingDispatcher;
        }

        public override void Execute(IMessageReceived<EventQueueMessage> messageReceived)
        {
            IEnumerable<IBlingHandler> blingHandlers = ((ImmediateBlingDispatcher) _blingDispatcher).Handlers;
            base.Execute(messageReceived);
        }

        protected override void Dispatch(object @event)
        {
            base.Dispatch(@event);
        }

        protected override object DeserializeEvent(Rewardle.Core.Infrastructure.EventQueue.EventQueueMessage message, Type eventType)
        {
            object deserializeEvent = base.DeserializeEvent(message, eventType);
            return deserializeEvent;
        }

        protected override void HandleException(IMessageReceived<EventQueueMessage> messageReceived, Exception ex)
        {
            base.HandleException(messageReceived, ex);
        }

        protected override Type GetTypeFromName(string typeName)
        {
            Type typeFromName = base.GetTypeFromName(typeName);
            return typeFromName;
        }
    }
}