﻿#region

using System.Collections.ObjectModel;
using System.Data;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.MSSqlServer;

#endregion

namespace Comrade.WebApi.Modules.Common
{
    /// <summary>
    /// </summary>
    public static class LoggingExtensions
    {
        public static void CreateLogMongoDb(LoggerProviderCollection providers)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.With(new ApplicationDetailsEnricher())
                .Enrich.FromLogContext()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MongoDB("mongodb://localhost/local")
                .WriteTo.Providers(providers)
                .CreateLogger();
        }

        public static void CreateLogSqlServer(LoggerProviderCollection providers,
            IConfigurationRoot configurationRoot)
        {
            var connection = configurationRoot.GetValue<string>("ConnectionStrings:MsSqlDb");

            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
                {
                    new("UserName", SqlDbType.VarChar)
                }
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.With(new ApplicationDetailsEnricher())
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connection,
                    new MSSqlServerSinkOptions
                    {
                        AutoCreateSqlTable = true,
                        TableName = "LogAPIContagem"
                    }, columnOptions: columnOptions)
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MongoDB("mongodb://localhost/local")
                .WriteTo.Providers(providers)
                .CreateLogger();
        }
    }
}