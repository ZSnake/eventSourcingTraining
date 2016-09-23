using Autofac;
using Rewardle.Boilerplate.Projections;
using Rewardle.Bootstrapping;

namespace Rewardle.Boilerplate.Infrastructure.BootstrapperTasks
{
    public class RegisterProjections : IBootstrapperTask
    {
        public void Do(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyTypes(typeof (TestProjection).Assembly)
                .AsImplementedInterfaces().AsSelf();
        }
    }
}