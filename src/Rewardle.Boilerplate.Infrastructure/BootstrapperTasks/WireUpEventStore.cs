using System;
using AcklenAvenue.EventSourcing;
using Autofac;
using Rewardle.Boilerplate.Domain.Services;
using Rewardle.Boilerplate.Domain.Services.Interfaces;
using Rewardle.Boilerplate.Domain.UserRoot;
using Rewardle.Bootstrapping;

namespace Rewardle.Boilerplate.Infrastructure.BootstrapperTasks
{
    public class WireUpEventStore : IBootstrapperTask
    {
        public void Do(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<TestRepository>().As<IAggregateRepository<TestAggregateRoot>>();
            //add here JsonEventConverter.CustomConversions

            containerBuilder.Register(
                x =>
                    new TestEventStore(EventStoreConfig.ConnectionString,
                        EventStoreConfig.TableName)).As<IEventStore<Guid>>();
        }
    }
}