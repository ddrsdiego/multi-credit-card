namespace MultiCreditCard.Users.Infra.Data.Statement
{
    public static class UserStatements
    {
        public const string GetUserFromCredentials= @"SELECT USERID, EMAIL, PASSWORD, USERNAME FROM USERS WITH(NOLOCK) WHERE EMAIL = @email AND PASSWORD = @password";

        public const string GetUserByUserId = @"SELECT * FROM USERS WHERE UserId = @userId";

        public const string GetUserByEmail = @"SELECT * FROM USERS u WHERE u.EMAIL = @email";

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
