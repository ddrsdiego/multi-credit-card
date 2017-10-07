namespace MultiCreditCard.CreditCards.Infra.Data.Statement
{
    public static class CreditCardStatements
    {

        public const string CreateNewCreditCard = @"
                                                    IF NOT EXISTS(SELECT 1 FROM CREDITCARDS WHERE CreditCardNumber = @creditCardNumber AND CreditCardType = @creditCardType AND UserId = @userId)
                                                    BEGIN
                                                        INSERT INTO CREDITCARDS(
                                                        CreditCardNumber
                                                        ,UserId
                                                        ,CreditCardType
                                                        ,PrintedName
                                                        ,PayDay
                                                        ,ExpirationDate
                                                        ,CreditLimit
                                                        ,CVV
                                                        ,CreateDate
                                                        ,Enable
                                                    )
                                                    VALUES(
                                                        @creditCardNumber
                                                        ,@userId
                                                        ,@creditCardType
                                                        ,@printedName
                                                        ,@payDay
                                                        ,@expirationDate
                                                        ,@creditLimit
                                                        ,@cVV
                                                        ,@createDate
                                                        ,@enable
                                                    )
                                                    END";

        public const string @UpdateCreditCarLimit = @"
                                                        UPDATE CREDITCARDS SET
	                                                        CreditLimit = @creditLimit
                                                        WHERE
                                                            ( USERID = @userId )
	                                                        AND ( CREDITCARDNUMBER = @creditcardnumber )
	                                                        AND ( CREDITCARDTYPE = @creditcardtype )";
    }
}
