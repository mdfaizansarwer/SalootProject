﻿using Core.Enums;
using Core.Setting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace SalootProject.Api.Configuration
{
    public static class SerilogConfiguration
    {
        public static void Register(IConfigurationRoot configuration)
        {
            var applicationSettings = configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>();
            var databaseSetting = applicationSettings.DatabaseSetting;
            var logSetting = applicationSettings.LogSetting;

            if (databaseSetting.DatabaseProvider == DatabaseProvider.Postgres)
            {
                PostgresRegistration(logSetting, databaseSetting.ConnectionStrings.Postgres);
            }

            else
            {
                SqlServerRegistration(logSetting, databaseSetting.ConnectionStrings.SqlServer);
            }
        }

        private static void PostgresRegistration(LogSetting logSetting, string connectionString)
        {
            Log.Logger = new LoggerConfiguration()
                        .WriteTo
                        .PostgreSQL(
                                    connectionString: connectionString,
                                    tableName: logSetting.TableName,
                                    restrictedToMinimumLevel: (LogEventLevel)logSetting.MinimumLevelSerilog,
                                    needAutoCreateTable: logSetting.AutoCreateSqlTable
                                    )
                        .CreateLogger();
        }

        private static void SqlServerRegistration(LogSetting logSetting, string connectionString)
        {

            var sqlServerSinkOptions = new MSSqlServerSinkOptions
            {
                TableName = logSetting.TableName,
                AutoCreateSqlTable = logSetting.AutoCreateSqlTable
            };

            Log.Logger = new LoggerConfiguration()
                        .WriteTo
                        .MSSqlServer(
                                    connectionString: connectionString,
                                    sinkOptions: sqlServerSinkOptions,
                                    restrictedToMinimumLevel: (LogEventLevel)logSetting.MinimumLevelSerilog
                                    )
                        .CreateLogger();
        }
    }
}
