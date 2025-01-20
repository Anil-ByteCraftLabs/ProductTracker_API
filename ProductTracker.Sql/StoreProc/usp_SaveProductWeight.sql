Create Procedure [dbo].[usp_SaveProductWeight]
(
    @ProductWeightId int,
    @Description Varchar(100),
    @IsActive bit,
    @CreatedBy nvarchar(400),
    @UpdatedBy nvarchar(400),
	@OrgId int = null
)
As
Begin
    If (@ProductWeightId > 0)
    Begin
        Update [Productweight]
        set [Description] = @Description,
            [IsActive] = @IsActive,
            UpdatedBy = @UpdatedBy,
            UpdatedOn = GETDATE(),
			OrgId = @OrgId
        Where Id = @ProductWeightId
    End
    Else
    Begin
        INSERT INTO [dbo].[Productweight]
        (
            [Description],
            [IsActive],
            [CreatedBy],
            [CreatedOn],
			[OrgId]
        )
        VALUES
        (@Description, 1, @CreatedBy, GetDate(), @OrgId)
    End
End

