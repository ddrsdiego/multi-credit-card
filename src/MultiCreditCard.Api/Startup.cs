using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MultiCreditCard.Api.Helpers;
using MultiCreditCard.Infra.IoC;
using MultiCreditCard.Shared.Config;

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
            services.AddJwtBearer(Configuration);
            services.AddMultiCreditCardServices();

            services.AddLogging();
            services.AddOptions();

            services.AddMediatR();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton<IConfiguration>(_ => Configuration);

            services.Configure<AuthConfig>(Configuration.GetSection(nameof(AuthConfig)));

            services.AddMvc();
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
