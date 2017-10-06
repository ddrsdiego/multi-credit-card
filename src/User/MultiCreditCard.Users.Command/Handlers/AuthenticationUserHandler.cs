using MediatR;
using MultiCreditCard.Users.Command.Commands;
using MultiCreditCard.Users.Command.Reponse;
using MultiCreditCard.Users.Domain.Contracts.Repositories;
using System;
using System.Threading.Tasks;

namespace MultiCreditCard.Users.Command.Handlers
{
    public class AuthenticationUserHandler : IAsyncRequestHandler<AuthenticationUserCommand, AuthenticationUserResponse>
    {
        private readonly IUserRepository _userRepository;


        public AuthenticationUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<AuthenticationUserResponse> Handle(AuthenticationUserCommand message)
        {
            throw new NotImplementedException();
        }
    }
}
