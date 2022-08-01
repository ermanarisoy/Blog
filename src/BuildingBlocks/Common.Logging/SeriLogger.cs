using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, configuration) =>
           {
               var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");

               configuration
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                    .ReadFrom.Configuration(context.Configuration);
           };
    }
}
