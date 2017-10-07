using MediatR;
using MultiCreditCard.Application.Common;

namespace MultiCreditCard.Users.Command.Commands
{
    public class RequestUpdateUserCreditLimitCommand : IRequest<Response>
    {
        public string UserId { get; set; }
        public decimal NewCreditLimit { get; set; }
        public Response Response { get; set; } = new Response();
    }
}
