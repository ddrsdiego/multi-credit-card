using MediatR;
using MultiCreditCard.Users.Command.Commands;
using MultiCreditCard.Users.Command.Reponse;
using MultiCreditCard.Users.Command.Validators;
using MultiCreditCard.Users.Domain.Contracts.Services;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Command.Handlers
{
    public class RegisterNewUserHandler : IAsyncRequestHandler<RegisterNewUserCommand, RegisterNewUserReponse>
    {
        private readonly IUserServices _userServices;
        private readonly IWalletService _walletService;
        private readonly RegisterNewUserValidator validator = new RegisterNewUserValidator();

        public RegisterNewUserHandler(IUserServices userServices, IWalletService walletService)
        {
            _userServices = userServices;
            _walletService = walletService;
        }

        public Task<RegisterNewUserReponse> Handle(RegisterNewUserCommand message)
        {
            var response = message.Response;

            ValidateCommand(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            RegisterNewUser(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            return Task.FromResult(response);
        }

        private void ValidateCommand(RegisterNewUserCommand command, RegisterNewUserReponse response)
        {
            var results = validator.Validate(command);
            var validationSucceeded = results.IsValid;

            if (!results.IsValid)
            {
                results.Errors.ToList().ForEach(x => response.AddError(x.ErrorMessage));
                return;
            }

            var user = _userServices.GetUserByEmail(command.Email).Result;
            if (user != null)
                response.AddError($"Usuário ja criado para o email {user.Email.EletronicAddress}");
        }

        private async void RegisterNewUser(RegisterNewUserCommand command, RegisterNewUserReponse response)
        {
            try
            {
                var newUser = AdapterCommantToEntity(command, response);
                if (response.HasError)
                    return;

                await _userServices.CreateUserAsync(newUser);
                await _walletService.CreateWalletAsync(newUser);
            }
            catch (Exception ex)
            {
                response.AddError($"Problemas ao salvar o usuário {command.UserName} - {ex.Message}");
            }
        }

        private User AdapterCommantToEntity(RegisterNewUserCommand command, RegisterNewUserReponse response)
        {
            var newUser = User.DefaultEntity();

            try
            {
                var email = new Email(command.Email);
                var password = new Password(command.Password);

                newUser = new User(command.UserName, command.DocumentNumber, email, password);
            }
            catch (Exception ex)
            {
                response.AddError($"Problemas a criar usuário. {ex.Message}");
            }

            return newUser;
        }
    }
}
