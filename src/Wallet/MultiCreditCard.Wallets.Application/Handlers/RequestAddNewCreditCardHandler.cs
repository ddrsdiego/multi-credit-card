using MediatR;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.Users.Application.Commands;
using MultiCreditCard.Users.Application.Reponse;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Application.Validators;
using MultiCreditCard.Wallets.Domain.Contracts.Repositories;
using MultiCreditCard.Wallets.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Application.Handlers
{
    public class RequestAddNewCreditCardHandler : IAsyncRequestHandler<RequestAddNewCreditCardCommand, RequestAddNewCreditCardResponse>
    {
        private User _user;
        private Wallet _wallet;

        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        private readonly RequestAddNewCreditCardValidator validator = new RequestAddNewCreditCardValidator();

        public RequestAddNewCreditCardHandler(IUserRepository userRepository, IWalletRepository walletRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
        }

        public Task<RequestAddNewCreditCardResponse> Handle(RequestAddNewCreditCardCommand message)
        {
            var response = message.Response;

            ValidateCommand(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            AddNewCreditCart(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            return Task.FromResult(response);
        }

        private void ValidateCommand(RequestAddNewCreditCardCommand command, RequestAddNewCreditCardResponse response)
        {
            var results = validator.Validate(command);
            var validationSucceeded = results.IsValid;

            if (!results.IsValid)
            {
                results.Errors.ToList().ForEach(x => response.AddError(x.ErrorMessage));
                return;
            }

            VerifyUser(command, response);
            if (response.HasError)
                return;

            VerifyHasWallet(command, response);
            if (response.HasError)
                return;
        }

        private void VerifyUser(RequestAddNewCreditCardCommand command, RequestAddNewCreditCardResponse response)
        {
            _user = _userRepository.GetUserByUserId(command.Userid).Result;
            if (string.IsNullOrEmpty(_user.UserId))
                response.AddError($"Usuário não localizado encontrado");
        }

        private void VerifyHasWallet(RequestAddNewCreditCardCommand command, RequestAddNewCreditCardResponse response)
        {
            _wallet = _walletRepository.GetWalletByUserId(_user.UserId).Result;
            if (_wallet == null)
                response.AddError($"Não há nenhuma carteira para o cliente.");
        }

        private CreditCard AdapterCommandToDomain(RequestAddNewCreditCardCommand command, RequestAddNewCreditCardResponse response)
        {
            var newCreditCard = CreditCard.DefaultEntity();

            try
            {
                newCreditCard = new CreditCard(_user, command.CreditCardType, command.CreditCardNumber, command.PrintedName, command.ExpirationDate, command.PayDay, command.CVV, command.CreditLimit);
            }
            catch (Exception ex)
            {
                response.AddError($"Não há nenhuma carteira para o cliente.");
            }

            return newCreditCard;
        }

        private void AddNewCreditCart(RequestAddNewCreditCardCommand command, RequestAddNewCreditCardResponse response)
        {
            try
            {
                _wallet.AddNewCreditCart(AdapterCommandToDomain(command, response));
                _walletRepository.AddNewCreditCart(_wallet);
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao adicionar o cartão de crédito a carteira. Erro: {ex.Message}");
            }
        }
    }
}