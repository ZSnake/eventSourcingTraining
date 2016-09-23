using System;
using System.Configuration;

namespace Rewardle.Boilerplate.Infrastructure
{
    public class ViewStoreConfig
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
                ConnectionStringSettings connectionStringSettings =
                    ConfigurationManager.ConnectionStrings["ViewStore_" + currentEnvironment];
                string connectionString = connectionStringSettings.ConnectionString;
                return connectionString;
            }
        }
    }
}