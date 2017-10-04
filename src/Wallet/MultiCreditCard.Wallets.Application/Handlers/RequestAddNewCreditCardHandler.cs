using MediatR;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.Users.Application.Commands;
using MultiCreditCard.Users.Application.Reponse;
using MultiCreditCard.Users.Domain.Contracts.Services;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Application.Validators;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using MultiCreditCard.Wallets.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Application.Handlers
{
    public class RequestAddNewCreditCardHandler : IAsyncRequestHandler<RequestAddNewCreditCardCommand, RequestAddNewCreditCardResponse>
    {
        private User _user;
        private Wallet _wallet;
        private readonly IUserServices _userServices;
        private readonly IWalletService _walletService;
        private readonly RequestAddNewCreditCardValidator validator = new RequestAddNewCreditCardValidator();

        public RequestAddNewCreditCardHandler(IUserServices userServices, IWalletService walletService)
        {
            _userServices = userServices;
            _walletService = walletService;
        }

        public Task<RequestAddNewCreditCardResponse> Handle(RequestAddNewCreditCardCommand message)
        {
            var response = message.Response;

            ValidateCommand(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            _wallet.AddNewCreditCart(AdapterCommandToDomain(message, response));

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

        private async void VerifyUser(RequestAddNewCreditCardCommand command, RequestAddNewCreditCardResponse response)
        {
            _user = await _userServices.GetUserByUserId(command.Userid);
            if (_user == null)
                response.AddError($"Usuário não localizado encontrado");
        }

        private async void VerifyHasWallet(RequestAddNewCreditCardCommand command, RequestAddNewCreditCardResponse response)
        {
            _wallet = await _walletService.GetWalletByUserId(_user.Email.EletronicAddress);
            if (_wallet == null)
                response.AddError($"Não há nenhuma carteira para o cliente.");
        }

        private CreditCard AdapterCommandToDomain(RequestAddNewCreditCardCommand command, RequestAddNewCreditCardResponse response)
        {
            var newCreditCard = CreditCard.DefaultEntity();

            try
            {
                newCreditCard = new CreditCard(command.CreditCardType, command.CreditCardNumber, command.PrintedName, command.ExpirationDate, command.PayDay, command.CVV, command.CreditLimit);
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
                _walletService.AddNewCreditCart(_wallet);
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao adicionar o cartão de crédito a carteira. Erro: {ex.Message}");
            }
        }
    }
}
