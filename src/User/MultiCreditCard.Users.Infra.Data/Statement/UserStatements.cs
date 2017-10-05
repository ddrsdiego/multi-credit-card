namespace MultiCreditCard.Users.Infra.Data.Statement
{
    public static class UserStatements
    {
        public const string GetUserByUserId = @"SELECT * FROM USERS WHERE UserId = @userId";
        public const string GetUserByEmail = @"SELECT UserId FROM USERS WHERE EMAIL = @email";
        public const string Create = @"
                                        INSERT INTO USERS(
	                                        UserId
	                                        ,UserName
	                                        ,DocumentNumber
	                                        ,Email
	                                        ,Password
	                                        ,CreationDate
                                        )
                                        VALUES(
	                                        @UserId
	                                        ,@UserName
	                                        ,@DocumentNumber
	                                        ,@Email
	                                        ,@Password
	                                        ,@CreationDate
                                        )";
    }
}
