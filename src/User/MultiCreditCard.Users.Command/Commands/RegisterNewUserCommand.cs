using MediatR;
using MultiCreditCard.Users.Command.Reponse;

namespace MultiCreditCard.Users.Command.Commands
{
    public class RegisterNewUserCommand : IRequest<RegisterNewUserReponse>
    {
        public RegisterNewUserCommand()
        {
            Response = new RegisterNewUserReponse();
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal DocumentNumber { get; set; }

        public RegisterNewUserReponse Response { get; set; }
    }
}
