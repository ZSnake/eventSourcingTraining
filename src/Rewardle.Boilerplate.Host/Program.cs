using Rewardle.Core.Infrastructure;
using Rewardle.Core.Infrastructure.Web;
using Topshelf;

namespace Rewardle.Boilerplate.Host
{
    class Program
    {
        public static void Main()
        {
            HostFactory.Run(x =>
                            {
                                x.Service<ServiceManager<Startup>>(s =>
                                                          {
                                                              s.ConstructUsing(
                                                                  name =>
                                                                      new ServiceManager<Startup>(Application.GetConfig("boilerplate-baseurl")));
                                                              s.WhenStarted(tc => tc.Start());
                                                              s.WhenStopped(tc => tc.Stop());
                                                          });
                                x.RunAsLocalSystem();

                                x.SetDescription("Rewardle.Boilerplate API Host");
                                x.SetDisplayName("Rewardle.Boilerplate.Api");
                                x.SetServiceName("Rewardle.Boilerplate.Api");
                            });
        }
    }
}