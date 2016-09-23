using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AcklenAvenue.EventSourcing;
using Rewardle.Boilerplate.Domain.Services.Interfaces;
using Rewardle.Boilerplate.Domain.UserRoot;
using Rewardle.Core.Domain;

namespace Rewardle.Boilerplate.Domain.Services
{
    public class TestRepository : IAggregateRepository<TestAggregateRoot>
    {
        readonly IAsyncEventDispatcher _dispatcher;

        readonly IEventStore<Guid> _eventStore;

        public TestRepository(IEventStore<Guid> eventStore, IAsyncEventDispatcher dispatcher)
        {
            _eventStore = eventStore;
            _dispatcher = dispatcher;
        }

        public async Task<T> Get<T>(Guid id) where T : TestAggregateRoot
        {
            IEnumerable<object> eventEnumerable;                
            try
            {
                eventEnumerable = await _eventStore.GetStream(id, ReturnShape.EventsOnly);
            }
            catch (Exception)
            {                
                throw;
            }
            List<object> events = eventEnumerable.ToList();
            if (!events.Any())
            {
                throw new TestAggregateRootNotFoundException(id);
            }
            ConstructorInfo ctor = typeof (T).GetConstructor(new[] {typeof (IEnumerable<object>)});
            if (ctor == null)
            {
                throw new Exception("The aggregate root was missing the ctor that accepts a collection of events.");
            }
            var instantiatedObject = ((T) ctor.Invoke(new object[] {events}));
            return instantiatedObject;
        }

        public async Task SaveChanges(TestAggregateRoot entity)
        {
            foreach (object change in entity.Changes)
            {
                await _eventStore.Persist(entity.Id, change);
                await _dispatcher.Dispatch(change);
            }
        }

        public async Task SaveChanges(DateTime datetimestamp, TestAggregateRoot entity)
        {
            foreach (object change in entity.Changes)
            {
                await _eventStore.Persist(datetimestamp, entity.Id, change);
                await _dispatcher.Dispatch(change);
            }
        }
    }
}