using Autofac;
using Rewardle.Bootstrapping;

namespace Rewardle.Boilerplate.Infrastructure.BootstrapperTasks
{
    public class ConfigureThreadExecutor : IBootstrapperTask
    {
        public void Do(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<BoilerplateCommandThreadExecutor>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<BoilerplateEventThreadExecutor>().AsSelf().InstancePerLifetimeScope();
        }
    }
}