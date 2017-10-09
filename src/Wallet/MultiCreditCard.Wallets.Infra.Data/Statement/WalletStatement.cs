namespace MultiCreditCard.Wallets.Infra.Data.Statement
{
    public static class WalletStatement
    {
        public const string CreateWallet = @"
                                            INSERT INTO WALLETS(
	                                            WalletId
	                                            ,UserId
	                                            ,UserCreditLimit
	                                            ,CreationDate
                                            )
                                            VALUES(
	                                            @WalletId
	                                            ,@UserId
	                                            ,@UserCreditLimit
	                                            ,@CreationDate
                                            )";

        public const string GetUserByUserId = @"SELECT * FROM USERS WITH(NOLOCK) WHERE USERID = @userId;";

        public const string GetCreditCardByUserId = @"SELECT * FROM CREDITCARDS WITH(NOLOCK) WHERE USERID = @userId AND ENABLE = 1;";

        public const string GetWalletByUserId = @"
                                                    SELECT * FROM USERS WITH(NOLOCK) WHERE USERID = @userId;
                                                    SELECT * FROM WALLETS WITH(NOLOCK) WHERE USERID = @userId;
                                                    SELECT * FROM CREDITCARDS WITH(NOLOCK) WHERE USERID = @userId AND ENABLE = 1;";

        public const string UpdateUserCreditLimit = @"UPDATE WALLETS SET UserCreditLimit = @UserCreditLimit WHERE WalletId = @WalletId";

        public const string AddNewCreditCart = @"
                                                    IF NOT EXISTS(SELECT 1 FROM WALLTES_CREDITCARDS WHERE ( WalletId = @walletId ) AND ( CreditCardId = @creditCardId ))
                                                    BEGIN
                                                        INSERT INTO WALLTES_CREDITCARDS(
	                                                        WalletId
	                                                        ,CreditCardId
                                                        )
                                                        VALUES(
	                                                        @walletId
	                                                        ,@creditCardId
                                                        )
                                                    END";

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
	                                                    )
                                                    END";
    }
}
