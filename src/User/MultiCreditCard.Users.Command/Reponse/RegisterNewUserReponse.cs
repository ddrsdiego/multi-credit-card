using MultiCreditCard.Application.Common;

namespace MultiCreditCard.Users.Command.Reponse
{
    public class RegisterNewUserReponse: Response
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public decimal DocumentNumber { get; set; }
        public string Email { get; set; }
    }
}