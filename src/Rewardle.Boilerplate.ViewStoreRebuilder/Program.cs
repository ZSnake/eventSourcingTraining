using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcklenAvenue.EventSourcing;
using Autofac;
using Rewardle.Boilerplate.Infrastructure;
using Rewardle.Boilerplate.Infrastructure.BootstrapperTasks;
using Rewardle.Boilerplate.Projections;
using Rewardle.Boilerplate.ViewStore;
using Rewardle.Bootstrapping;
using Rewardle.Core.Infrastructure;
using Rewardle.Core.Infrastructure.BootstrapperTasks;
using Rewardle.ViewStoreUtilities;

namespace Rewardle.Boilerplate.ViewStoreRebuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Using config: " + Application.GetCurrentEnvironment());

            IContainer container = GetContainer();
            Task<IEnumerable<object>> eventsResult = GetEvents(container);
            eventsResult.Wait();
            IEnumerable<object> events = eventsResult.Result;

            new ViewStoreRebuilder<ViewModelContext>(
                new ViewModelContext(Application.ViewStoreConfig.ConnectionString, new Log4NetLogger()))
                .WithAssembies(AppDomain.CurrentDomain.GetAssemblies())
                .WithEvents(events)
                .WithResolver(container.Resolve)
                .DropAndCreateDatabase()
                .Rebuild();
        }

        static async Task<IEnumerable<object>> GetEvents(IContainer container)
        {
            var eventStore = (TestEventStore) container.Resolve<IEventStore<Guid>>();
            IEnumerable<object> events = await eventStore.GetStream(ReturnShape.EventsOnly);
            return events;
        }

        static IContainer GetContainer()
        {
            var containerBuilder = new ContainerBuilder();

            BootstrapperTasks.RunAllInAssemblies(containerBuilder, typeof (WireUpEventStore).Assembly,
                typeof (WireUpDispatchers).Assembly);

            containerBuilder.RegisterAssemblyTypes(typeof (TestProjection).Assembly)
                .AsSelf();

            IContainer container = containerBuilder.Build();
            return container;
        }
    }
}