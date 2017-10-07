using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MultiCreditCard.Api.Helpers;
using MultiCreditCard.Infra.IoC;

namespace MultiCreditCard.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterJwtBearer();

            services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());

            services.AddLogging();

            services.AddMvc();
            services.AddMediatR(typeof(Startup));
            services.RegisterRepositories();
            services.RegisterServices();
            services.RegisterHandler();
            services.AddSingleton<IConfiguration>(_ => Configuration);
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net("log4net.config");

            app.UseAuthentication();
            app.UseMvc();

            app.Run(async context =>
            {
                var logger = loggerFactory.CreateLogger("MultiCreditCard.Startup");

                logger.LogInformation("Multi Credit Card is online");

                await context.Response.WriteAsync("Multi Credit Card is online");
            });
        }
    }
}
