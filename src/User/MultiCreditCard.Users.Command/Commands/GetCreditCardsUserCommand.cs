using MediatR;
using MultiCreditCard.Users.Command.Reponse;

namespace MultiCreditCard.Users.Command.Commands
{
    public class GetCreditCardsUserCommand : IRequest<GetCreditCardsUserResponse>
    {
        public GetCreditCardsUserCommand(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
        public GetCreditCardsUserResponse Response { get; set; } = new GetCreditCardsUserResponse();
    }
}
