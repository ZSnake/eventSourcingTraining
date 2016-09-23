using Autofac;
using Rewardle.Boilerplate.Domain.UserRoot.DomainEventHandlers;
using Rewardle.Bootstrapping;

namespace Rewardle.Boilerplate.Infrastructure.BootstrapperTasks
{
    public class RegisterEventHandlers : IBootstrapperTask
    {
        public void Do(ContainerBuilder containerBuilder)
        {            
            containerBuilder.RegisterAssemblyTypes(typeof (TestCreatedEventHandler).Assembly)
                .AsImplementedInterfaces();
        }
    }
}