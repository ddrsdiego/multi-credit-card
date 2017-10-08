using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MultiCreditCard.Users.Command.Handlers;
using MultiCreditCard.Wallets.Application.Handlers;

namespace MultiCreditCard.Infra.IoC
{
    public static class HandlersContainerEx
    {
        public static void RegisterHandler(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetCreditCardsUserHandler).Assembly,
                                typeof(AuthenticationUserHandler).Assembly,
                                typeof(RequestUpdateUserCreditLimitHandler).Assembly,
                                typeof(RequestCreditCardBuyHandler).Assembly, 
                                typeof(RegisterNewUserHandler).Assembly,
                                typeof(RequestAddNewCreditCardHandler).Assembly);
        }
    }
}
