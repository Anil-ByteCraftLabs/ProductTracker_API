
Create Procedure usp_validateCoupon
(
	@UniqueId Nvarchar(200)
)
AS
Begin
	If Exists(Select 1 from CouponsData where UniqueId = @UniqueId )
	Begin
		Declare @OrgAlias varchar(10) = ''
		SELECT @OrgAlias = SUBSTRING(@UniqueId, 17,  LEN(@UniqueId)) 
		Declare @BatchId Int = 0
		Select @BatchId = BatchId from CouponsData where UniqueId = @UniqueId 

		Select ProductName, Description, FssaiCode from ProductData PD
		Join BatchData BD on BD.ProductId = PD.Id
		Where Bd.Id = @BatchId

	End
End
