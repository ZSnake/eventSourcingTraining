using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;

namespace Rewardle.Boilerplate.Api
{
    [RoutePrefix("v1")]
    public class LogController : ApiController
    {
        [Route("raise")]
        [AcceptVerbs("GET")]
        public void RaiseException()
        {
            throw new Exception("A logging email should be sent as a result of this action.");
        }

        [Route("raise/validation")]
        [AcceptVerbs("GET")]
        public void RaiseValidation()
        {
            throw new CommandValidationException(new List<ValidationFailure>
                                                 {
                                                     new ValidationFailure("testProperty",
                                                         ValidationFailureType.Missing,
                                                         "This is a test validation failure")
                                                 });
        }

        [Route("logs")]
        [AcceptVerbs("GET")]
        public LogsResponse DebugLog()
        {
            FileAppender appender = GetFileAppender();
            if (appender == null) return new LogsResponse(new List<string> {"No log appenders found."});
            string logPath = appender.File;
            string newLogPath = logPath + ".copy";
            File.Copy(logPath, newLogPath, true);
            IEnumerable<string> lines = File.ReadAllLines(newLogPath).Reverse().Take(200).Reverse();
            return new LogsResponse(lines);
        }

        static IEnumerable<IAppender> GetAllAppenders()
        {
            var appenders = new List<IAppender>();
            var h = (Hierarchy) LogManager.GetRepository();
            appenders.AddRange(h.Root.Appenders.ToArray());
            return appenders;
        }

        static FileAppender GetFileAppender()
        {
            IEnumerable<IAppender> appenderList = GetAllAppenders();
            IEnumerable<RollingFileAppender> rollingFileAppenders = appenderList.OfType<RollingFileAppender>();
            return rollingFileAppenders.FirstOrDefault();
        }

        public class LogsResponse
        {
            public LogsResponse(IEnumerable<string> lines)
            {
                Lines = string.Join("\r\n", lines);
            }

            public string Lines { get; private set; }
        }
    }
}