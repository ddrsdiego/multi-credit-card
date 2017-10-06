using MediatR;
using MultiCreditCard.Application.Common;
using MultiCreditCard.Users.Command.Commands;
using MultiCreditCard.Users.Command.Validators;
using MultiCreditCard.Users.Domain.Contracts.Services;
using MultiCreditCard.Users.Domain.Entities;
using MultiCreditCard.Wallets.Domain.Contracts.Services;
using MultiCreditCard.Wallets.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Command.Handlers
{
    public class RequestUpdateUserCreditLimitHandler : IAsyncRequestHandler<RequestUpdateUserCreditLimitCommand, Response>
    {
        private User _user;
        private Wallet _wallet;

        private readonly IUserServices _userService;
        private readonly IWalletService _walletService;
        private readonly RequestUpdateUserCreditLimitValidator _validator;

        public RequestUpdateUserCreditLimitHandler(IWalletService walletService, IUserServices userService)
        {
            _walletService = walletService;
            _userService = userService;
            _validator = new RequestUpdateUserCreditLimitValidator();
        }

        public Task<Response> Handle(RequestUpdateUserCreditLimitCommand message)
        {
            var response = message.Response;

            ValidateCommand(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            UpdateUserCreditLimit(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            return Task.FromResult(response);
        }

        private void ValidateCommand(RequestUpdateUserCreditLimitCommand message, Response response)
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

        private void VerifyUser(RequestUpdateUserCreditLimitCommand command, Response response)
        {
            try
            {
                _user = _userService.GetUserByUserId(command.UserId).Result;
                if (string.IsNullOrEmpty(_user.UserId))
                    response.AddError($"Usuário não localizado encontrado");
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao pesquisar o usuário. {ex.Message}");
            }
        }

        private void VerifyHasWallet(RequestUpdateUserCreditLimitCommand command, Response response)
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

        private void UpdateUserCreditLimit(RequestUpdateUserCreditLimitCommand command, Response response)
        {
            try
            {
                _wallet.UpdateUserCreditLimit(command.NewCreditLimit);
                _walletService.UpdateUserCreditLimit(_wallet);
            }
            catch (Exception ex)
            {
                response.AddError($"Erro ao atualizar o valor de limite da carteira. {ex.Message}");
            }
        }
    }
}
