using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterJwtBearer();

            services.AddMvc();
            services.AddMediatR(typeof(Startup));
            services.RegisterRepositories();
            services.RegisterServices();
            services.RegisterHandler();
            services.AddSingleton<IConfiguration>(_ => Configuration);
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Multi Credit Cast is online");
            });
        }
    }
}
