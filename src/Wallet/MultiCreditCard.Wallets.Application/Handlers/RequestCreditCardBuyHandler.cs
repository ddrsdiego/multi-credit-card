using MediatR;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Application.Commands;
using MultiCreditCard.Wallets.Application.Reponse;
using MultiCreditCard.Wallets.Application.Validators;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using MultiCreditCard.Wallets.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Wallets.Application.Handlers
{
    public class RequestCreditCardBuyHandler : IAsyncRequestHandler<RequestCreditCardBuyCommand, RequestCreditCardBuyResponse>
    {
        private User _user;
        private Wallet _wallet;

        private readonly IWalletService _walletService;
        private readonly IUserRepository _userRepository;

        private readonly RequestCreditCardBuyValidator _validator = new RequestCreditCardBuyValidator();

        public RequestCreditCardBuyHandler(IUserRepository userRepository, IWalletService walletService)
        {
            _userRepository = userRepository;
            _walletService = walletService;
        }

        public Task<RequestCreditCardBuyResponse> Handle(RequestCreditCardBuyCommand message)
        {
            var response = message.Response;

            ValidateCommand(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            RequestBuy(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            return Task.FromResult(response);
        }

        private void ValidateCommand(RequestCreditCardBuyCommand message, RequestCreditCardBuyResponse response)
        {
            var results = _validator.Validate(message);
            if (!results.IsValid)
            {
                results.Errors.ToList().ForEach(x => response.AddError(x.ErrorMessage));
                return;
            }

            VerifyUser(message, response);
            if (response.HasError)
                return;

            VerifyHasWallet(message, response);
            if (response.HasError)
                return;
        }

        private void VerifyUser(RequestCreditCardBuyCommand command, RequestCreditCardBuyResponse response)
        {
            try
            {
                _user = _userRepository.GetUserByUserId(command.UserId).Result;
                if (string.IsNullOrEmpty(_user.UserId))
                    response.AddError($"Usuário não localizado encontrado");
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao pesquisar o usuário. {ex.Message}");
            }
        }

        private void VerifyHasWallet(RequestCreditCardBuyCommand command, RequestCreditCardBuyResponse response)
        {
            try
            {
                _wallet = _walletService.GetWalletByUserId(_user.UserId).Result;
                if (_wallet == null)
                    response.AddError($"Não há nenhuma carteira para o cliente.");
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao pesquisar recuperar dados da carteira. {ex.Message}");
            }
        }

        private void RequestBuy(RequestCreditCardBuyCommand command, RequestCreditCardBuyResponse response)
        {
            try
            {
                _wallet.Buy(command.AmountValue);
                _walletService.Buy(_wallet);
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao realizar a compra. {ex.Message}");
            }
        }
    }
}
