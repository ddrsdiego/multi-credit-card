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
                                                        DECLARE @filtroXml XML

                                                        SET @filtroXml = @xml

                                                        DECLARE @CreditCards TABLE(
	                                                        UserId				VARCHAR(36)
	                                                        ,CreditCardNumber	DECIMAL(18,0)
	                                                        ,CreditCardType		INT
	                                                        ,CreditLimit		DECIMAL(18,2)
                                                        )

                                                        INSERT INTO @CreditCards
                                                        SELECT
	                                                        CreditCards.col.value('@UserId'  ,'varchar(36)')                 [UserId]
	                                                        ,CreditCards.col.value('@CreditCardNumber'  , 'decimal(18,0)')   [CreditCardNumber]
	                                                        ,CreditCards.col.value('@CreditCardType'  , 'int')               [CreditCardType]
	                                                        ,CreditCards.col.value('@CreditLimit'  , 'decimal(18,2)')        [CreditLimit]
                                                        FROM @filtroXml.nodes('/creditCards/CreditCard') AS CreditCards(col)

                                                        UPDATE CREDITCARDS SET
	                                                        CREDITCARDS.CreditLimit = CreditCardsTmp.CreditLimit
                                                        FROM CREDITCARDS
	                                                        INNER JOIN @CreditCards CreditCardsTmp
		                                                        ON CreditCardsTmp.UserId = CREDITCARDS.UserId
		                                                        AND CreditCardsTmp.CreditCardNumber = CREDITCARDS.CreditCardNumber
		                                                        AND CreditCardsTmp.CreditCardType = CREDITCARDS.CreditCardType";


        public const string GetCreditCardsUser = @"SELECT * FROM VW_CREDITCARDS_USER WHERE USERID = @userId";

    }
}