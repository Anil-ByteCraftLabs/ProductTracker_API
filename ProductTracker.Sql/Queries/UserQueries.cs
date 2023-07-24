using System.Diagnostics.CodeAnalysis;

namespace ProductTracker.Sql.Queries
{
    [ExcludeFromCodeCoverage]
	public static class UserQueries
	{
		public static string AllUsers => "SELECT * FROM [User] (NOLOCK)";

		public static string UserById => "SELECT * FROM [User] (NOLOCK) WHERE [Id] = @UserId";

		public static string AddUser => "usp_SaveUser";



		public static string UpdateUser =>
			@"UPDATE [Contact] 
            SET [FirstName] = @FirstName, 
				[LastName] = @LastName, 
				[Email] = @Email, 
				[PhoneNumber] = @PhoneNumber
            WHERE [ContactId] = @ContactId";

		public static string DeleteUser => "DELETE FROM [User] WHERE [Id] = @Id";
	}
}
