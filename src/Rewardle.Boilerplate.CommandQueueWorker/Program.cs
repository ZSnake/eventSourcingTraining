using System.Reflection;
using AcklenAvenue.Poller;
using log4net;
using log4net.Config;
using Rewardle.Boilerplate.Infrastructure;
using Rewardle.Boilerplate.Infrastructure.BootstrapperTasks;
using Rewardle.Bootstrapping;
using Rewardle.Core.Infrastructure.BootstrapperTasks;
using Rewardle.Core.Infrastructure.Worker;
using Topshelf;

namespace Rewardle.Boilerplate.CommandQueueWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var builder = new PollerBuilder();
            builder.SetDescription("Rewardle's Boilerplate Commands Service")
                .SetDisplayName("Rewardle.Boilerplate.Commands")
                .SetServiceName("Rewardle.Boilerplate.Commands")
                .OverideServiceConfiguration(configurator => configurator.RunAsLocalSystem())
                .RegisterComponents(b => BootstrapperTasks.RunAllInAssemblies(b, typeof (WireUpEventStore).Assembly,
                typeof(WireUpDispatchers).Assembly, typeof(WireUpThreadFactory).Assembly))
                .OnException((sender, ex) =>
                             {
                                 ILog logger = LogManager.GetLogger(sender.GetType());
                                 if (ex is TargetInvocationException)
                                 {
                                     ex = ex.InnerException;
                                 }
                                 logger.Error(ex.Message, ex);
                                 return ex;
                             })
                .WithTask<AsyncCommandQueueWorker<BoilerplateCommandThreadExecutor>>(
                    "BoilerplateCommandWorker", "Process the commands of the Boilerplate services", 2)
                .Build()
                .Start();
        }
    }
}