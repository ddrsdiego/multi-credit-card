using MultiCreditCard.Application.Common;

namespace MultiCreditCard.Users.Command.Reponse
{
    public class AuthenticationUserResponse : Response
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
