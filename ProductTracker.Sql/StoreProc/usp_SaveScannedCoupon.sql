Create Procedure [dbo].[usp_SaveScannedCoupon] (
  @CouponCode nvarchar(50),
  @ScannedBy nvarchar(30),
  @Latitude Float,
  @Longitude Float
) 
As
Begin 
	Declare @OrgAlias varchar(10) = ''
	SELECT @OrgAlias = SUBSTRING(@CouponCode, 17,  LEN(@CouponCode)) 
	Insert into ScanHistory (CouponCode,ScannedBy, ScannedOn, [Location], OrgAlias) Values
	(@CouponCode, @ScannedBy, Getdate(), GEOGRAPHY::Point(@Latitude, @Longitude, 4326), @OrgAlias )
End

