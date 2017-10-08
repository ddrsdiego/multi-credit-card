using Microsoft.Extensions.DependencyInjection;

namespace MultiCreditCard.Infra.IoC
{
    public static class MultiCreditCardRegisterContainers
    {
        public static void AddMultiCreditCardServices(this IServiceCollection services)
        {
            services.RegisterHandler();
            services.RegisterServices();
            services.RegisterRepositories();
        }
    }
}
