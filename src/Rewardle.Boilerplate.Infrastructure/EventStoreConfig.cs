using System;
using System.Configuration;

namespace Rewardle.Boilerplate.Infrastructure
{
    public class EventStoreConfig
    {
        const string EnvironmentKey = "Environment";

        public static string GetCurrentEnvironment()
        {
            return GetConfigItem(EnvironmentKey);
        }

        static string GetConfigItem(string environmentKey)
        {
            return Environment.GetEnvironmentVariable(environmentKey)
                   ?? ConfigurationManager.AppSettings[environmentKey];
        }

        public static string ConnectionString
        {
            get
            {
                string currentEnvironment = GetCurrentEnvironment();
                return ConfigurationManager.ConnectionStrings["EventStore_" + currentEnvironment].ConnectionString;
            }
        }

        public static string TableName
        {
            get
            {
                return GetConfigItem("eventStoreTableName");
            }
        }
    }
}