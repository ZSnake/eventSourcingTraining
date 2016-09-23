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

namespace Rewardle.Boilerplate.EventQueueWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            var builder = new PollerBuilder();
            builder.SetDescription("Event Queue Worker for Rewardle.Boilerplate")
                .SetDisplayName("Rewardle.Boilerplate.Events")
                .SetServiceName("Rewardle.Boilerplate.Events")
                .OverideServiceConfiguration(configurator => configurator.RunAsLocalSystem())
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
                .RegisterComponents(b => BootstrapperTasks.RunAllInAssemblies(b, typeof (WireUpEventStore).Assembly,
                    typeof(WireUpDispatchers).Assembly, typeof(WireUpThreadFactory).Assembly))
                .WithTask<AsyncEventWorker<BoilerplateEventThreadExecutor>>(
                    "BoilerplateEventWorker", "Process the events of the Boilerplate services", 2)
                .Build()
                .Start();
        }
    }
}