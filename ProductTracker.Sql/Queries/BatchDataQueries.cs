﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Sql.Queries
{
    [ExcludeFromCodeCoverage]
    public class BatchDataQueries
    {
		public static string AllBatches => "usp_GetBatchData";
		public static string AllUserBatches => "usp_GetUserBatches";


		public static string BatchById => "SELECT * FROM [BatchData] (NOLOCK) WHERE [Id] = @OrgId";

		public static string AddBatch =>
			@"INSERT INTO [Contact] ([FirstName], [LastName], [Email], [PhoneNumber]) 
				VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";

		public static string UpdateBatch =>
			@"UPDATE [Contact] 
            SET [FirstName] = @FirstName, 
				[LastName] = @LastName, 
				[Email] = @Email, 
				[PhoneNumber] = @PhoneNumber
            WHERE [ContactId] = @ContactId";

		public static string DeleteBatch => "DELETE FROM [BatchData] WHERE [Id] = @BatchId";

		public static string SaveBatchData => "usp_SaveBatchData";
	}
}
