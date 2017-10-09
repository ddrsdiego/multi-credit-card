namespace MultiCreditCard.CreditCards.Infra.Data.Statement
{
    public static class CreditCardStatements
    {

        public const string CreateNewCreditCard = @"
                                                    IF NOT EXISTS(SELECT 1 FROM CREDITCARDS WHERE CreditCardNumber = @creditCardNumber AND CreditCardType = @creditCardType AND UserId = @userId)
                                                    BEGIN
                                                        INSERT INTO CREDITCARDS(
                                                            CreditCardId
                                                            ,CreditCardNumber
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
                                                            @creditCardId
                                                            ,@creditCardNumber
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


        public const string UpdateCreditCardLimit = @"
                                                        DECLARE @filtroXml XML

                                                        SET @filtroXml = @xml

                                                        DECLARE @CreditCards TABLE(
                                                            CreditCardId        VARCHAR(36)    
	                                                        ,CreditLimit		DECIMAL(18,2)
                                                        )

                                                        INSERT INTO @CreditCards
                                                        SELECT
                                                            CreditCards.col.value('@CreditCardId'  ,'varchar(36)')               [CreditCardId]
	                                                        ,CreditCards.col.value('@CreditLimit'  , 'decimal(18,2)')            [CreditLimit]
                                                        FROM @filtroXml.nodes('/creditCards/CreditCard') AS CreditCards(col)

                                                        UPDATE CREDITCARDS SET
	                                                        CREDITCARDS.CreditLimit = CreditCardsTmp.CreditLimit
                                                        FROM CREDITCARDS
	                                                        INNER JOIN @CreditCards CreditCardsTmp
		                                                        ON CreditCardsTmp.CreditCardId = CREDITCARDS.CreditCardId";
        
        public const string GetCreditCardsUser = @"SELECT * FROM VW_CREDITCARDS_USER WHERE USERID = @userId";

    }
}