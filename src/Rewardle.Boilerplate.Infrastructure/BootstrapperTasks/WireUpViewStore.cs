using Autofac;
using Rewardle.Boilerplate.Domain;
using Rewardle.Boilerplate.Domain.Services.Interfaces;
using Rewardle.Boilerplate.ViewStore;
using Rewardle.Bootstrapping;

namespace Rewardle.Boilerplate.Infrastructure.BootstrapperTasks
{
    public class WireUpViewStore : IBootstrapperTask
    {
        public void Do(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<ViewModelContext>()
                .As<IViewStore>().As<IViewModelWriter>()
                .WithParameter("connectionString", ViewStoreConfig.ConnectionString);
        }
    }
}