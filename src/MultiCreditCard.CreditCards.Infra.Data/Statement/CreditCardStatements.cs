namespace MultiCreditCard.CreditCards.Infra.Data.Statement
{
    public static class CreditCardStatements
    {
        public const string @UpdateCreditCarLimit = @"
                                                        UPDATE CREDITCARDS SET
	                                                        CreditLimit = @creditLimit
                                                        WHERE
	                                                        CREDITCARDNUMBER = @creditcardnumber
	                                                        AND CREDITCARDTYPE = @creditcardtype";
    }
}
