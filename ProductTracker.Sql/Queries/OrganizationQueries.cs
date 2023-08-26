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
		public static string GetDataBase => "SELECT DBPath FROM [Organization] where AliasName =  @OrgId";

		public static string OrganizationById => "SELECT * FROM [Organization] (NOLOCK) WHERE [Id] = @OrgId";


		public static string SaveOrganization => "usp_SaveOrganization";

	

		public static string DeleteOrganization => "DELETE FROM [Organization] WHERE [Id] = @OrgId";

	}
}
