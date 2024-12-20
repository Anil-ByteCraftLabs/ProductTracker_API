If Not Exists(SELECT 1 FROM sys.columns WHERE OBJECT_ID = OBJECT_ID('ProductType')  AND name = 'OrgId')
Begin
	ALTER TABLE ProductType
	ADD  OrgId int
End

GO

Alter  PROCEDURE [dbo].[Usp_saveproducttype] (@ProductTypeId INT,
                                              @Description   VARCHAR(100),
                                              @IsActive      BIT,
											  @OrgId INT,
                                              @CreatedBy     NVARCHAR(400),
                                              @UpdatedBy     NVARCHAR(400))
AS
  BEGIN
      IF ( @ProductTypeId > 0 )
        BEGIN
            UPDATE [product type]
            SET    [description] = @Description,
                   [isactive] = @IsActive,
				   OrgId = @OrgId,
                   updatedby = @UpdatedBy,
                   updatedon = Getdate()
            WHERE  id = @ProductTypeId
        END
      ELSE
        BEGIN
            INSERT INTO [dbo].[producttype]
                        ([description],
                         [isactive],
						 [OrgId],
                         [createdby],
                         [createdon])
            VALUES      ( @Description,
                          1,
						  @OrgId,
                          @CreatedBy,
                          Getdate() )
        END
  END
  
  GO

  
Create Procedure Usp_BatchFilteredData
(
	@OrgId int = 0,
	@StartDate Datetime = Null,
	@EndDate Datetime = null,
	@ProductTypeId int = 0,
	@PlantId Int = 0
)
AS
Begin
	Set @StartDate = IIF(@StartDate = '', Null,@StartDate)
	Set @EndDate = IIF(@EndDate = '', Null,@EndDate)

	Select BD.Id, BD.Name 'BatchName', BD.BatchNo, BD.PlantId, P.PlantName,
	PD.ProductName, PD.Id ProductId, BD.NoOfCoupons 'NumberOfCoupon', BD.NoOfPrintedCoupons 'NoOfPrintedCoupons' ,
	BD.Status,
	BD.CreatedBy, BD.CreatedOn, BD.UpdatedBy, BD.UpdatedOn
	from BatchData BD
	Join Admin.dbo.Plant P on BD.PlantId = P.Id
	Join Admin.dbo.Organization O on P.Orgid = O.Id
	Join ProductData PD on BD.ProductId = PD.Id
	Join ProductCategory PC on PD.CategoryId = PC.Id
	Join ProductType PT on PC.ProductTypeId = PT.Id

	Where O.Id = @OrgId
	 AND( @StartDate IS NULL OR  (bd.createdon BETWEEN @StartDate AND    @EndDate))
	 AND (   @ProductTypeId = 0 OR PC.ProductTypeId=  @ProductTypeId )
	 AND (   @PlantId = 0 OR P.Id=  @PlantId )


End

GO