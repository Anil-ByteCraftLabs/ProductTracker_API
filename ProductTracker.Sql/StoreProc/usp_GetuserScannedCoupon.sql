Create Procedure usp_GetuserScannedCoupon 
(
	@Userid varchar(30)
)
As
Begin

	Select Id, CouponCode, ScannedOn, OrgAlias,   Location.Lat AS Latitude, Location.Long AS Longitude
	from ScanHistory Where ScannedBy = @Userid
End


