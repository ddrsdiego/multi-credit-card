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

        public const string GetWalletByUserId = @"SELECT WALLETID FROM WALLETS WHERE UserId = @UserId";

        public const string UpdateUserCreditLimit = @"UPDATE WALLETS SET UserCreditLimit = @UserCreditLimit WHERE WalletId = @WalletId";
    }
}
