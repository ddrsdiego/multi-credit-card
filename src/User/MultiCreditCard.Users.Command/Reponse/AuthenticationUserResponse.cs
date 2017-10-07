using MultiCreditCard.Application.Common;
using System;

namespace MultiCreditCard.Users.Command.Reponse
{
    public class AuthenticationUserResponse : Response
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
