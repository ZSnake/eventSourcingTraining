using Autofac;
using Rewardle.Boilerplate.Domain.Services.Interfaces;
using Rewardle.Bootstrapping;

namespace Rewardle.Boilerplate.Infrastructure.BootstrapperTasks
{
    public class DomainServiceRegistrationBootstrapperTask : IBootstrapperTask
    {
        public void Do(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(typeof (IAggregateRepository<>).Assembly)
                .AsImplementedInterfaces();
        }
    }
}