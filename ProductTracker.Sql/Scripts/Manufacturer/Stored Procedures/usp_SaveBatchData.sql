
Create Procedure [dbo].[usp_SaveBatchData]
(
	@Name Varchar(100)
	,@BatchNo Varchar(50)
	,@NoOfCoupons Int
	,@OrgId Int
	,@CreatedBy Int

)
As
Begin

INSERT INTO [dbo].[BatchData]
           ([Name]
           ,[BatchNo]
           ,[NoOfCoupons]
           ,[NoOfPrintedCoupons]
           ,[Status]
           ,[IsActive]
           ,[OrgId]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (
			@Name,
			@BatchNo,
			@NoOfCoupons,
			0,
			1,
			1,
			@OrgId,
			@CreatedBy,
			GetDate()
		   )

End



