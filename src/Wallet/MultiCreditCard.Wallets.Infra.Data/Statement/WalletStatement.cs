namespace MultiCreditCard.Wallets.Infra.Data.Statement
{
    public static class WalletStatement
    {
        public const string CreateWallet = @"
                                            INSERT INTO WALLETS(
	                                            WalletId
	                                            ,UserId
	                                            ,AvailableCredit
	                                            ,MaximumCreditLimit
	                                            ,UserCreditLimit
	                                            ,CreationDate
                                            )
                                            VALUES(
	                                            @WalletId
	                                            ,@UserId
	                                            ,@AvailableCredit
	                                            ,@MaximumCreditLimit
	                                            ,@UserCreditLimit
	                                            ,@CreationDate
                                            )";

        public const string GetWalletByUserId = @"
                                                    SELECT *
                                                    FROM WALLETS wallet WITH(NOLOCK)
	                                                    LEFT JOIN WALLTES_CREDITCARDS WITH(NOLOCK)
		                                                    ON WALLTES_CREDITCARDS.WalletId = wallet.WalletId
	                                                    LEFT JOIN CREDITCARDS creditCard WITH(NOLOCK)
		                                                    ON creditCard.CreditCardNumber = WALLTES_CREDITCARDS.CreditCardNumber
                                                    WHERE wallet.UserId = @UserId";

        public const string UpdateUserCreditLimit = @"UPDATE WALLETS SET UserCreditLimit = @UserCreditLimit WHERE WalletId = @WalletId";

        public const string AddNewCreditCart = @"
                                                    IF NOT EXISTS(SELECT 1 FROM WALLTES_CREDITCARDS WHERE WalletId = @walletId AND CreditCardNumber = @creditCardNumber)
                                                    BEGIN
                                                        INSERT INTO WALLTES_CREDITCARDS(
	                                                        WalletId
	                                                        ,CreditCardNumber
                                                        )
                                                        VALUES(
	                                                        @walletId
	                                                        ,@creditCardNumber
                                                        )
                                                    END";

        public const string CreateNewCreditCard = @"
                                                    IF NOT EXISTS(SELECT 1 FROM CREDITCARDS WHERE CreditCardNumber = @creditCardNumber AND CreditCardType = @creditCardType)
                                                    BEGIN
	                                                    INSERT INTO CREDITCARDS(
		                                                    CreditCardNumber
		                                                    ,CreditCardType
		                                                    ,PrintedName
		                                                    ,PayDay
		                                                    ,ExpirationDate
		                                                    ,CreditLimit
		                                                    ,CVV
                                                            ,CreateDate
	                                                    )
	                                                    VALUES(
		                                                    @creditCardNumber
		                                                    ,@creditCardType
		                                                    ,@printedName
		                                                    ,@payDay
		                                                    ,@expirationDate
		                                                    ,@creditLimit
		                                                    ,@cVV
                                                            ,@createDate
	                                                    )
                                                    END";
    }
}
