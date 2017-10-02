using MediatR;
using MultiCreditCard.Users.Command.Commands;
using MultiCreditCard.Users.Command.Reponse;
using MultiCreditCard.Users.Command.Validators;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Users.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Command.Handlers
{
    public class RegisterNewUserHandler : IAsyncRequestHandler<RegisterNewUserCommand, RegisterNewUserReponse>
    {
        private User _user;
        private readonly IUserRepository _userRepository;
        private readonly RegisterNewUserValidator validator = new RegisterNewUserValidator();

        public RegisterNewUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<RegisterNewUserReponse> Handle(RegisterNewUserCommand message)
        {
            var response = message.Response;

            ValidateCommand(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            AdapterCommantToEntity(message, response);
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
                results.Errors.ToList().ForEach(x => response.AddError(x.ErrorMessage));

            var user = _userRepository.GetUserByEmail(command.Email).Result;
            if (user != null)
                response.AddError($"Usuário ja criado para o email {command.Email}");
        }

        private void RegisterNewUser(RegisterNewUserCommand command, RegisterNewUserReponse response)
        {
            try
            {
                _userRepository.CreateAsync(_user).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.AddError($"Problemas ao salvar o usuário {command.UserName} - {ex.Message}");
            }
        }

        private void AdapterCommantToEntity(RegisterNewUserCommand command, RegisterNewUserReponse response)
        {
            try
            {
                var email = new Email(command.Email);
                var password = new Password(command.Password);

                _user = new User(command.UserName, command.DocumentNumber, email, password);
            }
            catch (Exception ex)
            {
                response.AddError($"Problemas a criar usuário. {ex.Message}");
            }
        }
    }
}
