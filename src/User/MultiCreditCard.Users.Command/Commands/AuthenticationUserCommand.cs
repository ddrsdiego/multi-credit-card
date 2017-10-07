using MediatR;
using MultiCreditCard.Users.Command.Reponse;

namespace MultiCreditCard.Users.Command.Commands
{
    public class AuthenticationUserCommand : IRequest<AuthenticationUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public AuthenticationUserResponse Response { get; set; } = new AuthenticationUserResponse();

    }
}
