using Autofac;
using Rewardle.Boilerplate.Domain.Services;
using Rewardle.Bootstrapping;

namespace Rewardle.Boilerplate.Infrastructure.BootstrapperTasks
{
    public class WireUpDependencies : IBootstrapperTask {
        public void Do(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Log4NetLogger>().As<ILogger>();
        }
    }
}