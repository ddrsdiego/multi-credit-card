namespace MultiCreditCard.Users.Domain.ValueObjects
{
    public class Password
    {
        public Password(string password)
        {
            Raw = password;
            Encoded = password;
        }

        public string Encoded { get; private set; }
        public string Raw { get; private set; }
    }
}
