using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public static class OrganizationQueries
    {
		public static string AllOrganization => "SELECT * FROM [Organization] (NOLOCK)";

		public static string OrganizationById => "SELECT * FROM [Organization] (NOLOCK) WHERE [Id] = @OrgId";

		//public static string AddOrganization =>
		//	@"INSERT INTO [Contact] ([FirstName], [LastName], [Email], [PhoneNumber]) 
		//		VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";

		public static string SaveOrganization => "usp_SaveOrganization";

		public static string UpdateOrganization =>
			@"UPDATE [Contact] 
            SET [FirstName] = @FirstName, 
				[LastName] = @LastName, 
				[Email] = @Email, 
				[PhoneNumber] = @PhoneNumber
            WHERE [ContactId] = @ContactId";

		public static string DeleteOrganization => "DELETE FROM [Contact] WHERE [Id] = @OrgId";

	}
}
