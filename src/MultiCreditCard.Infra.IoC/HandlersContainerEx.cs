using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MultiCreditCard.Users.Command.Handlers;

namespace MultiCreditCard.Infra.IoC
{
    public static class HandlersContainerEx
    {
        public static void RegisterHandler(this IServiceCollection services)
        {
             services.AddMediatR(typeof(RegisterNewUserHandler).Assembly);
        }
    }
}
