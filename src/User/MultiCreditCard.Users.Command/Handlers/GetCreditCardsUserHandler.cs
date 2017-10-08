using MediatR;
using MultiCreditCard.CreditCards.Domain.Contracts.Repositories;
using MultiCreditCard.CreditCards.Domain.Entities;
using MultiCreditCard.CreditCards.Domain.Enums;
using MultiCreditCard.Users.Command.Commands;
using MultiCreditCard.Users.Command.Reponse;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using MultiCreditCard.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Command.Handlers
{
    public class GetCreditCardsUserHandler : IAsyncRequestHandler<GetCreditCardsUserCommand, GetCreditCardsUserResponse>
    {
        private User _user;
        private IList<CreditCard> _creditCards;

        private readonly IUserRepository _userRepository;
        private readonly ICreditCardRepository _creditCardRepository;

        public GetCreditCardsUserHandler(IUserRepository userRepository, ICreditCardRepository creditCardRepository)
        {
            _user = User.DefaultEntity();

            _userRepository = userRepository;
            _creditCardRepository = creditCardRepository;
        }

        public Task<GetCreditCardsUserResponse> Handle(GetCreditCardsUserCommand message)
        {
            var response = message.Response;

            VerifyUser(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            GetCreditCards(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            GetResponse(message, response);
            if (response.HasError)
                return Task.FromResult(response);

            return Task.FromResult(response);
        }

        private void VerifyUser(GetCreditCardsUserCommand command, GetCreditCardsUserResponse response)
        {
            _user = _userRepository.GetUserByUserId(command.UserId).Result;
            if (string.IsNullOrEmpty(_user.UserId))
            {
                response.AddError($"Usuário não localizado.");
                return;
            }
        }

        private void GetCreditCards(GetCreditCardsUserCommand command, GetCreditCardsUserResponse response)
        {
            try
            {
                _creditCards = _creditCardRepository.GetCreditCardByUserId(command.UserId);
            }
            catch (Exception ex)
            {
                response.AddError($"Usuário ao consultar os cartões de crédito. {ex.Message}");
            }
        }

        private void GetResponse(GetCreditCardsUserCommand command, GetCreditCardsUserResponse response)
        {
            response.UserId = _user.UserId;
            response.DataTimeQuery = DateTime.Now;

            if (_creditCards != null && _creditCards.Any())
            {
                _creditCards.ToList().ForEach(creditCard =>
                {
                    response.CreditCards.Add(new CreditCardsResponse
                    {
                        CreditCardNumber = creditCard.CreditCardNumber,
                        CreditCardType = ((CreditCardType)creditCard.CreditCardType).ToString(),
                        CreditLimit = creditCard.CreditLimit,
                        ExpirationDate = creditCard.ExpirationDate,
                        PayDay = creditCard.PayDay,
                    });
                });
            }
        }
    }
}